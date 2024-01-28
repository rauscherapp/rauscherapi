using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterSymbolsQueryHandler : IRequestHandler<ObterSymbolsQuery, Symbols>
  {
    private readonly ILogger<ObterSymbolsQueryHandler> _logger;
    private readonly ISymbolsRepository _symbolsRepository;
    public ObterSymbolsQueryHandler(ILogger<ObterSymbolsQueryHandler> logger, ISymbolsRepository symbolsRepository)
    {
      _symbolsRepository = symbolsRepository;
      _logger = logger;
    }
    public async Task<Symbols> Handle(ObterSymbolsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request.ID);

      return _symbolsRepository.ObterSymbols(request.ID);

    }
  }
}
