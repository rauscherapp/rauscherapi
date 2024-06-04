using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace APIs.Security.JWT;

public class AccessManager
{
  private UserManager<ApplicationUser> _userManager;
  private SignInManager<ApplicationUser> _signInManager;
  private SigningConfigurations _signingConfigurations;
  private TokenConfigurations _tokenConfigurations;

  public AccessManager(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager,
      SigningConfigurations signingConfigurations,
      TokenConfigurations tokenConfigurations)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _signingConfigurations = signingConfigurations;
    _tokenConfigurations = tokenConfigurations;
  }

  public async Task<(UserResponse? user, bool isValid)> ValidateCredentials(UserRequest user)
  {
    bool credenciaisValidas = false;
    var userResponse = new UserResponse();
    if (user is not null && !String.IsNullOrWhiteSpace(user.Email))
    {
      // Verifica a existência do usuário nas tabelas do
      // ASP.NET Core Identity
      var userIdentity = await _userManager
          .FindByEmailAsync(user.Email);
      if (userIdentity is not null)
      {
        // Efetua o login com base no Id do usuário e sua senha
        var resultadoLogin = await _signInManager
            .CheckPasswordSignInAsync(userIdentity, user.Password!, false);
        if (resultadoLogin.Succeeded)
        {
          // Verifica se o usuário em questão possui
          // a role Acesso-APIs
          credenciaisValidas = _userManager.IsInRoleAsync(
              userIdentity, Roles.ROLE_ACESSO_APIS!).Result;
        }
        user.Avatar = userIdentity.Avatar;
        user.Status = userIdentity.Status;
        user.Name = userIdentity.NomeCompleto;
        userResponse.Email = userIdentity.Email;
        userResponse.Name = userIdentity.NomeCompleto;
        userResponse.HasValidStripeSubscription = userIdentity.HasValidStripeSubscription;
      }
    }

    return (userResponse, credenciaisValidas);
  }
  public async Task<bool> CreateUser(UserRequest userRequest)
  {
    bool userCreated = false;
    var userResponse = await _userManager.FindByEmailAsync(userRequest.Email!);
    if (userResponse is null)
    {
      var user = new ApplicationUser()
      {
        Email = userRequest.Email,
        EmailConfirmed = true,
        UserName = userRequest.Email        
      };

      if (userRequest.Password != null)
      {
        var resultado = await _userManager
                  .CreateAsync(user, userRequest.Password);

        if (resultado.Succeeded)
        {
          userCreated = true;
          _userManager.AddToRoleAsync(user, Roles.ROLE_ACESSO_APIS).Wait();
        }
      }
    }
    return userCreated;
  }

  public async Task<bool> CheckSubscription(UserRequest userRequest)
  {
    var user = await _userManager.FindByEmailAsync(userRequest.Email!);
    return user?.HasValidStripeSubscription ?? false;
  }



  public Token GenerateToken(UserResponse user)
  {
    ClaimsIdentity identity = new(
        new GenericIdentity(user.Email!, "Login"),
        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email!)
        }
    );

    DateTime dataCriacao = DateTime.Now;
    DateTime dataExpiracao = dataCriacao +
        TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

    var handler = new JwtSecurityTokenHandler();
    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
    {
      Issuer = _tokenConfigurations.Issuer,
      Audience = _tokenConfigurations.Audience,
      SigningCredentials = _signingConfigurations.SigningCredentials,
      Subject = identity,
      NotBefore = dataCriacao,
      Expires = dataExpiracao
    });
    var token = handler.WriteToken(securityToken);

    return new()
    {
      Authenticated = true,
      Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
      Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
      AccessToken = token,
      Message = "OK",
      User = user
    };
  }
}