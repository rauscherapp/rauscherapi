using Domain.Models;
using Domain.Queries;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.QueryHandlers
{
  public class ObterAboutUsQueryHandler : IRequestHandler<ObterAboutUsQuery, AboutUs>
  {
    private readonly ILogger<ObterAboutUsQueryHandler> _logger;
    private readonly IAboutUsRepository _aboutUsRepository;
    public ObterAboutUsQueryHandler(ILogger<ObterAboutUsQueryHandler> logger, IAboutUsRepository aboutUsRepository)
    {
      _aboutUsRepository = aboutUsRepository;
      _logger = logger;
    }
    public async Task<AboutUs> Handle(ObterAboutUsQuery request, CancellationToken cancellationToken)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(Handle));

      return _aboutUsRepository.ObterAboutUs();
    }
  }
}
