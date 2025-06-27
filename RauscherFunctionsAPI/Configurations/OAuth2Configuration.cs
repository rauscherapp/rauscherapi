using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI.Configurations
{
    public static class OAuth2Configuration
    {
        private const string KEYCLOAK_CLIENT_ID = "SiStocklerEmailSender";
        private const string URL = "https://tshp.stocklerltda.com.br:8543/auth/realms/SiStockler";

        public static void AddOAuth2Configuration(this IServiceCollection services)
        {

            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.Authority = URL;
                config.IncludeErrorDetails = true;
                config.SaveToken = true;
                config.Audience = KEYCLOAK_CLIENT_ID;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = URL,
                    ValidateLifetime = true
                };
                config.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "text/plain";
                        return context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(context.Exception.Message), 0, context.Exception.Message.Length);
                    },
                    OnTokenValidated = context =>
                    {
                        MapKeycloakRolesToRoleClaims(context);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static void MapKeycloakRolesToRoleClaims(TokenValidatedContext context)
        {
            var resourceAccess = JObject.Parse(context.Principal.FindFirst("resource_access").Value);

            if (resourceAccess != null)
            {
                var clientResource = resourceAccess[KEYCLOAK_CLIENT_ID];

                if (clientResource != null)
                {
                    var clientRoles = clientResource["roles"];
                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    if (claimsIdentity == null)
                    {
                        return;
                    }

                    foreach (var clientRole in clientRoles)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, clientRole.ToString()));
                    }
                }
            }
        }
    }
}
