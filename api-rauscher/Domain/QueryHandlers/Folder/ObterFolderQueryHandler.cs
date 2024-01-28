using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterFolderQueryHandler : IRequestHandler<ObterFolderQuery, Folder>
  {
    private readonly ILogger<ObterFolderQueryHandler> _logger;
    private readonly IFolderRepository _folderRepository;
    public ObterFolderQueryHandler(ILogger<ObterFolderQueryHandler> logger, IFolderRepository folderRepository)
    {
      _folderRepository = folderRepository;
      _logger = logger;
    }
    public async Task<Folder> Handle(ObterFolderQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request.IDFolder);

      return _folderRepository.GetFolder(request.IDFolder);

    }
  }
}
