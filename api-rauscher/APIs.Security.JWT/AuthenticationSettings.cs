namespace APIs.Security.JWT;

public sealed class AuthenticationSettings
{
    public const string SectionName = "Authentication";

    public LegacyJwtSettings LegacyJwt { get; set; } = new();

    public ExternalIdentityProviderSettings ExternalIdentityProvider { get; set; } = new();
}

public sealed class LegacyJwtSettings
{
    public string Audience { get; set; } = "rauscher-idei";

    public string Issuer { get; set; } = "RauscherApp";

    public int AccessTokenLifetimeSeconds { get; set; } = 3600;

    public string? SecretJwtKey { get; set; }

    public bool AllowRuntimeGeneratedSecret { get; set; } = true;
}

public sealed class ExternalIdentityProviderSettings
{
    public bool Enabled { get; set; }

    public string? Authority { get; set; }

    public string? Audience { get; set; }

    public string? ClientId { get; set; }
}
