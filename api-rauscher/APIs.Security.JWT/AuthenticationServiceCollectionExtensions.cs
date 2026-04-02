using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APIs.Security.JWT;

public static class AuthenticationServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguredJwtSecurity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = BuildAuthenticationSettings(configuration);
        var tokenConfigurations = BuildTokenConfigurations(settings.LegacyJwt);

        services.AddSingleton(settings);
        return services.AddJwtSecurity(tokenConfigurations);
    }

    internal static AuthenticationSettings BuildAuthenticationSettings(IConfiguration configuration)
    {
        var authSection = configuration.GetSection(AuthenticationSettings.SectionName);
        var legacyJwtSection = authSection.GetSection(nameof(AuthenticationSettings.LegacyJwt));
        var externalIdentityProviderSection = authSection.GetSection(nameof(AuthenticationSettings.ExternalIdentityProvider));

        return new AuthenticationSettings
        {
            LegacyJwt = new LegacyJwtSettings
            {
                Audience = legacyJwtSection[nameof(LegacyJwtSettings.Audience)] ?? "rauscher-idei",
                Issuer = legacyJwtSection[nameof(LegacyJwtSettings.Issuer)] ?? "RauscherApp",
                SecretJwtKey = legacyJwtSection[nameof(LegacyJwtSettings.SecretJwtKey)],
                AccessTokenLifetimeSeconds = TryGetInt(
                    legacyJwtSection[nameof(LegacyJwtSettings.AccessTokenLifetimeSeconds)],
                    3600),
                AllowRuntimeGeneratedSecret = TryGetBool(
                    legacyJwtSection[nameof(LegacyJwtSettings.AllowRuntimeGeneratedSecret)],
                    true)
            },
            ExternalIdentityProvider = new ExternalIdentityProviderSettings
            {
                Enabled = TryGetBool(
                    externalIdentityProviderSection[nameof(ExternalIdentityProviderSettings.Enabled)],
                    false),
                Authority = externalIdentityProviderSection[nameof(ExternalIdentityProviderSettings.Authority)],
                Audience = externalIdentityProviderSection[nameof(ExternalIdentityProviderSettings.Audience)],
                ClientId = externalIdentityProviderSection[nameof(ExternalIdentityProviderSettings.ClientId)]
            }
        };
    }

    internal static TokenConfigurations BuildTokenConfigurations(LegacyJwtSettings settings)
    {
        var tokenConfigurations = new TokenConfigurations
        {
            Audience = settings.Audience,
            Issuer = settings.Issuer,
            Seconds = settings.AccessTokenLifetimeSeconds,
            SecretJwtKey = settings.SecretJwtKey
        };

        if (!string.IsNullOrWhiteSpace(tokenConfigurations.SecretJwtKey))
        {
            return tokenConfigurations;
        }

        if (!settings.AllowRuntimeGeneratedSecret)
        {
            throw new InvalidOperationException(
                "Authentication:LegacyJwt:SecretJwtKey must be configured when runtime secret generation is disabled.");
        }

        tokenConfigurations.GenerateSecretJwtKey();
        return tokenConfigurations;
    }

    private static int TryGetInt(string? value, int defaultValue)
    {
        return int.TryParse(value, out var parsedValue) ? parsedValue : defaultValue;
    }

    private static bool TryGetBool(string? value, bool defaultValue)
    {
        return bool.TryParse(value, out var parsedValue) ? parsedValue : defaultValue;
    }
}
