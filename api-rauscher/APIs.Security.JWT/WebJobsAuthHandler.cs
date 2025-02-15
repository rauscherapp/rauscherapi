using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace APIs.Security.JWT
{
  public class WebJobsAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
  {
    public WebJobsAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
      // Retorna "NoResult" se não precisar autenticar
      return Task.FromResult(AuthenticateResult.NoResult());
    }
  }

}
