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
  public class ListarSymbolsQueryHandler : IRequestHandler<ListarSymbolsQuery, IQueryable<Symbols>>
  {
    private readonly ILogger<ListarSymbolsQueryHandler> _logger;
    private readonly ISymbolsRepository _symbolsRepository;
    public ListarSymbolsQueryHandler(ILogger<ListarSymbolsQueryHandler> logger, ISymbolsRepository symbolsRepository)
    {
      _symbolsRepository = symbolsRepository;
      _logger = logger;
    }
    public async Task<IQueryable<Symbols>> Handle(ListarSymbolsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      return await _symbolsRepository.ListarSymbolss(request.Parameters);

    }
  }
}
