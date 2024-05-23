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
	        public class ListarAppParametersQueryHandler : IRequestHandler<ListarAppParametersQuery, PagedList<AppParameters>>
	{
		            private readonly ILogger<ListarAppParametersQueryHandler> _logger;
		            private readonly IAppParametersRepository _appparametersRepository;
		            public ListarAppParametersQueryHandler(ILogger<ListarAppParametersQueryHandler> logger, IAppParametersRepository appparametersRepository)
		{
			                _appparametersRepository = appparametersRepository;
			                _logger = logger;
		}
		            public async Task<PagedList<AppParameters>> Handle(ListarAppParametersQuery request, CancellationToken cancellationToken)
		{
			                _logger.LogInformation("Handling: {MethodName} | params: {@Request}" , nameof(Handle), request);
			
			                return await _appparametersRepository.ListarAppParameterss(request.Parameters);
			
		}
	}
}
