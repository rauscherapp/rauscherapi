using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterAppParametersQueryHandler : IRequestHandler<ObterAppParametersQuery, AppParameters>
  {
    private readonly ILogger<ObterAppParametersQueryHandler> _logger;
    private readonly IAppParametersRepository _appparametersRepository;
    public ObterAppParametersQueryHandler(ILogger<ObterAppParametersQueryHandler> logger, IAppParametersRepository appparametersRepository)
    {
      _appparametersRepository = appparametersRepository;
      _logger = logger;
    }
    public async Task<AppParameters> Handle(ObterAppParametersQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName} | params: {@Request}", nameof(Handle), request.AppParametersId);

      return _appparametersRepository.ObterAppParameters(request.AppParametersId);

    }
  }
}
