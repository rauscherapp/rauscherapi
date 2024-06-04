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
  public class ListarEventRegistryQueryHandler : IRequestHandler<ListarEventRegistryQuery, PagedList<EventRegistry>>
  {
    private readonly ILogger<ListarEventRegistryQueryHandler> _logger;
    private readonly IEventRegistryRepository _eventregistryRepository;
    public ListarEventRegistryQueryHandler(ILogger<ListarEventRegistryQueryHandler> logger, IEventRegistryRepository eventregistryRepository)
    {
      _eventregistryRepository = eventregistryRepository;
      _logger = logger;
    }
    public async Task<PagedList<EventRegistry>> Handle(ListarEventRegistryQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      return await _eventregistryRepository.ListarEventRegistrys(request.Parameters);

    }
  }
}
