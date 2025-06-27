using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ListarPostQueryHandler : IRequestHandler<ListarPostQuery, IQueryable<Post>>
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
    public async Task<IQueryable<Post>> Handle(ListarPostQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      if (!string.IsNullOrEmpty(request.Parameters.folder))
      {
        // Dicionário de traduções (exemplo)
        var translationDictionary = new Dictionary<string, string>
        {
            { "economia", "economy" },
            { "pimenta preta", "black pepper" },
            { "café", "coffee" },
            { "meteorologia", "meteorology" },
            // Adicione outras traduções conforme necessário
        };

        // Traduzir o parâmetro folder
        var folderInEnglish = request.Parameters.folder;
        if (translationDictionary.ContainsKey(folderInEnglish.ToLower()))
        {
          folderInEnglish = translationDictionary[folderInEnglish.ToLower()];
        }

        var folderId = _folderRepository.GetFoldersBySlug(folderInEnglish).ID;
        return await _postRepository.ListarPostsByFolderId(request.Parameters, folderId);
      }

      return await _postRepository.ListarPosts(request.Parameters);
    }
  }
}
