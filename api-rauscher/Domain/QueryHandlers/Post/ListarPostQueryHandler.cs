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
  public class ListarPostQueryHandler : IRequestHandler<ListarPostQuery, PagedList<Post>>
  {
    private readonly ILogger<ListarPostQueryHandler> _logger;
    private readonly IPostRepository _postRepository;
    private readonly IFolderRepository _folderRepository;
    public ListarPostQueryHandler(ILogger<ListarPostQueryHandler> logger, IPostRepository postRepository, IFolderRepository folderRepository = null)
    {
      _postRepository = postRepository;
      _logger = logger;
      _folderRepository = folderRepository;
    }
    public async Task<PagedList<Post>> Handle(ListarPostQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      if(!string.IsNullOrEmpty(request.Parameters.folder))
      {
        var folderId = _folderRepository.GetFoldersBySlug(request.Parameters.folder).ID;
        return await _postRepository.ListarPostsByFolderId(request.Parameters, folderId);
      }

      return await _postRepository.ListarPosts(request.Parameters);

    }
  }
}
