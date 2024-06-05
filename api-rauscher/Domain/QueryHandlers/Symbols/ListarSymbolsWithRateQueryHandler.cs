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
  public class ListarSymbolsWithRateQueryHandler : IRequestHandler<ListarSymbolsWithRateQuery, PagedList<Symbols>>
  {
    private readonly ILogger<ListarSymbolsQueryHandler> _logger;
    private readonly ISymbolsRepository _symbolsRepository;
    public ListarSymbolsWithRateQueryHandler(ILogger<ListarSymbolsQueryHandler> logger, ISymbolsRepository symbolsRepository)
    {
      _symbolsRepository = symbolsRepository;
      _logger = logger;
    }
    public async Task<PagedList<Symbols>> Handle(ListarSymbolsWithRateQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request);

      return await _symbolsRepository.GetSymbolsWithLatestRatesAsync(request.Parameters);

    }
  }
}
