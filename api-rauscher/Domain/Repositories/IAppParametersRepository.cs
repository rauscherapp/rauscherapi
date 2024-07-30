using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IAppParametersRepository : IRepository<AppParameters>
	{
		Task<PagedList<AppParameters>> ListarAppParameterss(AppParametersParameters parameters);
    AppParameters ObterAppParameters();
  }
}
