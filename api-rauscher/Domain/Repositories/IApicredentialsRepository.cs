using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IApiCredentialsRepository : IRepository<ApiCredentials>
	{
		Task<PagedList<ApiCredentials>> ListarApiCredentialss(ApiCredentialsParameters parameters);
		ApiCredentials ObterApiCredentials(string apiKey);
		void RemoveApiCredentials(string apiKey);

  }
}
