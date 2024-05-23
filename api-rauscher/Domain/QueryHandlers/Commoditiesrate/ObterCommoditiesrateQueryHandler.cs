using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterCommoditiesRateQueryHandler : IRequestHandler<ObterCommoditiesRateQuery, CommoditiesRate>
  {
    private readonly ILogger<ObterCommoditiesRateQueryHandler> _logger;
    private readonly ICommoditiesRateRepository _CommoditiesRateRepository;
    public ObterCommoditiesRateQueryHandler(ILogger<ObterCommoditiesRateQueryHandler> logger, ICommoditiesRateRepository CommoditiesRateRepository)
    {
      _CommoditiesRateRepository = CommoditiesRateRepository;
      _logger = logger;
    }
    public async Task<CommoditiesRate> Handle(ObterCommoditiesRateQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request.Id);

      return _CommoditiesRateRepository.ObterCommoditiesRate(request.Id);

    }
  }
}
