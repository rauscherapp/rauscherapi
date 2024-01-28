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
	public class ApiCredentialsRepository : Repository<ApiCredentials>, IApiCredentialsRepository
	{
		public ApiCredentialsRepository(RauscherDbContext context): base(context)
		{
		}
		public ApiCredentials ObterApiCredentials(string apiKey)
		{
			    var Apicredentials = Db.ApiCredentialss
			        .Where(c => c.Apikey == apiKey);
			
			    return Apicredentials.FirstOrDefault();
		}
		public virtual void RemoveApiCredentials(string apiKey)
		{
			    Db.ApiCredentialss.Remove(Db.ApiCredentialss.Find(apiKey));			
		}
		
		public async Task<PagedList<ApiCredentials>> ListarApiCredentialss(ApiCredentialsParameters parameters)
		{
			var apicredentials = Db.ApiCredentialss
			.AsQueryable();
			
			if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
			    apicredentials = apicredentials.ApplySort(parameters.OrderBy);
			
			return PagedList<ApiCredentials>.Create(apicredentials, parameters.PageNumber, parameters.PageSize);
		}
	}
}
