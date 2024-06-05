using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface ICommoditiesRateRepository : IRepository<CommoditiesRate>
	{
    Task<CommoditiesRate> GetLastPriceBeforeTimestamp(string commodityCode, long timestamp);
		CommoditiesRate ObterCommoditiesRate(Guid id);
    Task<PagedList<CommoditiesRate>> ListarCommoditiesRates(CommoditiesRateParameters parameters);

  }
}
