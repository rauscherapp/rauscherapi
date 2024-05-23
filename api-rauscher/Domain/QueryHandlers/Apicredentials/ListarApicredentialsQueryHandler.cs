using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ListarApiCredentialsQueryHandler : IRequestHandler<ListarApiCredentialsQuery, PagedList<ApiCredentials>>
  {
    private readonly ILogger<ListarApiCredentialsQueryHandler> _logger;
    private readonly IApiCredentialsRepository _apicredentialsRepository;
    public ListarApiCredentialsQueryHandler(ILogger<ListarApiCredentialsQueryHandler> logger, IApiCredentialsRepository apicredentialsRepository)
    {
      _apicredentialsRepository = apicredentialsRepository;
      _logger = logger;
    }
    public async Task<PagedList<ApiCredentials>> Handle(ListarApiCredentialsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      return await _apicredentialsRepository.ListarApiCredentialss(request.Parameters);

    }
  }
}
