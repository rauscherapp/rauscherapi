using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace APIs.Security.JWT
{
  public static class JwtSecurityExtension
  {
    public static IServiceCollection AddJwtSecurity(
        this IServiceCollection services,
        TokenConfigurations tokenConfigurations)
    {
      // Configuração do ASP.NET Identity
      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        // Configurações de senha
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 1;
      })
      .AddEntityFrameworkStores<ApiSecurityDbContext>()
      .AddDefaultTokenProviders();

      // Configurando a dependência para a classe de validação
      services.AddScoped<IAccessManager, AccessManager>();
      services.AddScoped<JwtSecurityExtensionEvents>();

      // Configuração da assinatura de tokens
      var signingConfigurations = new SigningConfigurations(tokenConfigurations.SecretJwtKey!);
      services.AddSingleton(signingConfigurations);
      services.AddSingleton(tokenConfigurations);

      // Configuração da autenticação
      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(bearerOptions =>
      {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = signingConfigurations.Key;
        paramsValidation.ValidAudience = tokenConfigurations.Audience;
        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

        // Validações de segurança
        paramsValidation.ValidateIssuerSigningKey = true;
        paramsValidation.ValidateLifetime = true;

        // Tempo de tolerância para a expiração do token
        paramsValidation.ClockSkew = TimeSpan.Zero;

        bearerOptions.EventsType = typeof(JwtSecurityExtensionEvents);
      }).AddScheme<AuthenticationSchemeOptions, WebJobsAuthHandler>(
    "WebJobsAuthLevel",
    "WebJobsAuthLevel",
    options => { }); 


      // Configuração de autorização
      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build());
      });

      return services;
    }
  }
}
