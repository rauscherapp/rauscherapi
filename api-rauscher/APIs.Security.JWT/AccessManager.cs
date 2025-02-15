using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace APIs.Security.JWT;

public class AccessManager : IAccessManager
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly SigningConfigurations _signingConfigurations;
  private readonly TokenConfigurations _tokenConfigurations;

  public AccessManager(
      UserManager<ApplicationUser> userManager,
      SigningConfigurations signingConfigurations,
      TokenConfigurations tokenConfigurations)
  {
    _userManager = userManager;
    _signingConfigurations = signingConfigurations;
    _tokenConfigurations = tokenConfigurations;
  }

  public async Task<(UserResponse? user, bool isValid)> ValidateCredentials(UserRequest user)
  {
    bool credenciaisValidas = false;
    var userResponse = new UserResponse();

    if (!string.IsNullOrWhiteSpace(user.Email))
    {
      var userIdentity = await _userManager.FindByEmailAsync(user.Email);
      if (userIdentity != null && await _userManager.CheckPasswordAsync(userIdentity, user.Password!))
      {
        credenciaisValidas = await _userManager.IsInRoleAsync(userIdentity, Roles.ROLE_ACESSO_APIS);
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
    var existingUser = await _userManager.FindByEmailAsync(userRequest.Email!);
    if (existingUser != null)
    {
      await _userManager.DeleteAsync(existingUser);
    }

    var user = new ApplicationUser()
    {
      Email = userRequest.Email,
      EmailConfirmed = true,
      UserName = userRequest.Email
    };

    if (userRequest.Password != null)
    {
      var resultado = await _userManager.CreateAsync(user, userRequest.Password);

      if (resultado.Succeeded)
      {
        userCreated = true;
        await _userManager.AddToRoleAsync(user, Roles.ROLE_ACESSO_APIS);
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
    var identity = new ClaimsIdentity(new GenericIdentity(user.Email!, "Login"), new[]
    {
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
      new Claim(JwtRegisteredClaimNames.UniqueName, user.Email!)
    });

    var dataCriacao = DateTime.UtcNow;
    var dataExpiracao = dataCriacao.AddSeconds(_tokenConfigurations.Seconds);

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

    return new Token
    {
      Authenticated = true,
      Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
      Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
      AccessToken = handler.WriteToken(securityToken),
      Message = "OK",
      User = user
    };
  }
}
