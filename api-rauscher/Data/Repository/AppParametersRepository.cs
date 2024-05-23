using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class AppParametersRepository : Repository<AppParameters>, IAppParametersRepository
	{
		public AppParametersRepository(RauscherDbContext context): base(context)
		{
		}
		public AppParameters ObterAppParameters(Guid appParametersId)
		{
			    var AppParameters = Db.AppParameters
			        .Where(c => c.Id == appParametersId);
			
			    return AppParameters.FirstOrDefault();
		}
		
		public async Task<PagedList<AppParameters>> ListarAppParameterss(AppParametersParameters parameters)
		{
			var appparameters = Db.AppParameters
			.AsQueryable();
			
			if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
			    appparameters = appparameters.ApplySort(parameters.OrderBy);
			
			return PagedList<AppParameters>.Create(appparameters, parameters.PageNumber, parameters.PageSize);
		}
	}
}
