using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterApiCredentialsQueryHandler : IRequestHandler<ObterApiCredentialsQuery, ApiCredentials>
  {
    private readonly ILogger<ObterApiCredentialsQueryHandler> _logger;
    private readonly IApiCredentialsRepository _apicredentialsRepository;
    public ObterApiCredentialsQueryHandler(ILogger<ObterApiCredentialsQueryHandler> logger, IApiCredentialsRepository apicredentialsRepository)
    {
      _apicredentialsRepository = apicredentialsRepository;
      _logger = logger;
    }
    public async Task<ApiCredentials> Handle(ObterApiCredentialsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request.ApiKey);

      return _apicredentialsRepository.ObterApiCredentials(request.ApiKey);
    }
  }
}
