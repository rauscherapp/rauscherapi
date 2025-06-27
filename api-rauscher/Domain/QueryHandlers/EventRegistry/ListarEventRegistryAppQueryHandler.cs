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
  public class ListarEventRegistryAppQueryHandler : IRequestHandler<ListarEventRegistryAppQuery, PagedList<EventRegistry>>
  {
    private readonly ILogger<ListarEventRegistryAppQueryHandler> _logger;
    private readonly IEventRegistryRepository _eventregistryRepository;
    public ListarEventRegistryAppQueryHandler(ILogger<ListarEventRegistryAppQueryHandler> logger, IEventRegistryRepository eventregistryRepository)
    {
      _eventregistryRepository = eventregistryRepository;
      _logger = logger;
    }
    public async Task<PagedList<EventRegistry>> Handle(ListarEventRegistryAppQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      return await _eventregistryRepository.ListarEventRegistryApp(request.Parameters);

    }
  }
}
