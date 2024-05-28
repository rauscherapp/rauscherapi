using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterEventRegistryQueryHandler : IRequestHandler<ObterEventRegistryQuery, EventRegistry>
  {
    private readonly ILogger<ObterEventRegistryQueryHandler> _logger;
    private readonly IEventRegistryRepository _eventregistryRepository;
    public ObterEventRegistryQueryHandler(ILogger<ObterEventRegistryQueryHandler> logger, IEventRegistryRepository eventregistryRepository)
    {
      _eventregistryRepository = eventregistryRepository;
      _logger = logger;
    }
    public async Task<EventRegistry> Handle(ObterEventRegistryQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request.EventRegistryId);

      return _eventregistryRepository.ObterEventRegistry(request.EventRegistryId);

    }
  }
}
