using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
	        public class ListarCommoditiesRateQueryHandler : IRequestHandler<ListarCommoditiesRateQuery, PagedList<CommoditiesRate>>
	{
		            private readonly ILogger<ListarCommoditiesRateQueryHandler> _logger;
		            private readonly ICommoditiesRateRepository _CommoditiesRateRepository;
		            public ListarCommoditiesRateQueryHandler(ILogger<ListarCommoditiesRateQueryHandler> logger, ICommoditiesRateRepository CommoditiesRateRepository)
		{
			                _CommoditiesRateRepository = CommoditiesRateRepository;
			                _logger = logger;
		}
		            public async Task<PagedList<CommoditiesRate>> Handle(ListarCommoditiesRateQuery request, CancellationToken cancellationToken)
		{
			                _logger.LogInformation("Handling: {MethodName} | params: {@Request}" , nameof(Handle), request);
			
			                return await _CommoditiesRateRepository.ListarCommoditiesRates(request.Parameters);
			
		}
	}
}
