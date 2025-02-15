using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterAppParametersQueryHandler : IRequestHandler<ObterAppParametersQuery, AppParameters>
  {
    private readonly ILogger<ObterAppParametersQueryHandler> _logger;
    private readonly IAppParametersRepository _appParametersRepository;
    public ObterAppParametersQueryHandler(ILogger<ObterAppParametersQueryHandler> logger, IAppParametersRepository appParametersRepository)
    {
      _appParametersRepository = appParametersRepository;
      _logger = logger;
    }
    public async Task<AppParameters> Handle(ObterAppParametersQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: ObterAppParametersQueryHandler");
      try
      {
        return _appParametersRepository.ObterAppParameters();
      }
      catch (Exception ex)
      {
        _logger.LogError($"ERROR:{ex.Message}");
        _logger.LogError($"ERROR:{ex.InnerException}");
        return null;
      }

    }
  }
}
