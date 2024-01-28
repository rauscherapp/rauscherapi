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
	        public class ListarApicredentialsQueryHandler : IRequestHandler<ListarApicredentialsQuery, PagedList<ApiCredentials>>
	{
		            private readonly ILogger<ListarApicredentialsQueryHandler> _logger;
		            private readonly IApiCredentialsRepository _apicredentialsRepository;
		            public ListarApicredentialsQueryHandler(ILogger<ListarApicredentialsQueryHandler> logger, IApiCredentialsRepository apicredentialsRepository)
		{
			                _apicredentialsRepository = apicredentialsRepository;
			                _logger = logger;
		}
		            public async Task<PagedList<ApiCredentials>> Handle(ListarApicredentialsQuery request, CancellationToken cancellationToken)
		{
			                _logger.LogInformation("Handling: {MethodName} | params: {@Request}" , nameof(Handle), request);
			
			                return await _apicredentialsRepository.ListarApiCredentialss(request.Parameters);
			
		}
	}
}
