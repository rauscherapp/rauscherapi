using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
	        public class ObterPostQueryHandler : IRequestHandler<ObterPostQuery, Post>
	{
		            private readonly ILogger<ObterPostQueryHandler> _logger;
		            private readonly IPostRepository _postRepository;
		            public ObterPostQueryHandler(ILogger<ObterPostQueryHandler> logger, IPostRepository postRepository)
		{
			                _postRepository = postRepository;
			                _logger = logger;
		}
		            public async Task<Post> Handle(ObterPostQuery request, CancellationToken cancellationToken)
		{
			                _logger.LogInformation("Handling: {MethodName} | params: {@Request}" , nameof(Handle), request.Id);
			
			                return _postRepository.ObterPost(request.Id);
			
		}
	}
}
