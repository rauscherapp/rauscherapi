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
  public class ListarFolderQueryHandler : IRequestHandler<ListarFolderQuery, PagedList<Folder>>
  {
    private readonly ILogger<ListarFolderQueryHandler> _logger;
    private readonly IFolderRepository _folderRepository;
    public ListarFolderQueryHandler(ILogger<ListarFolderQueryHandler> logger, IFolderRepository folderRepository)
    {
      _folderRepository = folderRepository;
      _logger = logger;
    }
    public async Task<PagedList<Folder>> Handle(ListarFolderQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      return await _folderRepository.GetFolders(request.Parameters);

    }
  }
}
