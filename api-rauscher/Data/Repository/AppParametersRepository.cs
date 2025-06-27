using Data.Context;
using Domain.Models;
using Domain.QueryHandlers;
using Domain.QueryParameters;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class AppParametersRepository : Repository<AppParameters>, IAppParametersRepository
  {
    private readonly ILogger<AppParametersRepository> _logger;
    public AppParametersRepository(RauscherDbContext context, ILogger<AppParametersRepository> logger) : base(context)
    {
      _logger = logger;
    }
    public AppParameters ObterAppParameters()
    {
      var appParameters = Db.AppParameters;

      return appParameters.FirstOrDefault();
    }

    public async Task<PagedList<AppParameters>> ListarAppParameterss(AppParametersParameters parameters)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(ListarAppParameterss));
      var appParameters = Db.AppParameters
      .AsQueryable();

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        appParameters = appParameters.ApplySort(parameters.OrderBy);

      return PagedList<AppParameters>.Create(appParameters, parameters.PageNumber, parameters.PageSize);
    }
  }
}
