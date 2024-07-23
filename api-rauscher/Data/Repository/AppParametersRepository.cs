using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
  public class AppParametersRepository : Repository<AppParameters>, IAppParametersRepository
  {
    public AppParametersRepository(RauscherDbContext context) : base(context)
    {
    }
    public AppParameters ObterAppParameters()
    {
      var appParameters = Db.AppParameters;

      return appParameters.FirstOrDefault();
    }

    public async Task<PagedList<AppParameters>> ListarAppParameterss(AppParametersParameters parameters)
    {
      var appParameters = Db.AppParameters
      .AsQueryable();

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
        appParameters = appParameters.ApplySort(parameters.OrderBy);

      return PagedList<AppParameters>.Create(appParameters, parameters.PageNumber, parameters.PageSize);
    }
  }
}
