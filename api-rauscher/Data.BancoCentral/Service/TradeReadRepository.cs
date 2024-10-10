using Data.BancoCentral.Api.Interfaces;
using Data.BancoCentral.Api.Mapping;
using Data.BancoCentral.Api.Model;
using Data.Commodities.Api.Mapping;
using Domain.Models;
using Domain.Repositories;

namespace Data.BancoCentral.Api.Service
{
  public class TradeReadRepository : Repository, ITradeReadRepository
  {

    private readonly IBancoCentralAPI _bancoCentralAPI;
    public TradeReadRepository(IBancoCentralAPI bancoCentralAPI)
    {
      _bancoCentralAPI = bancoCentralAPI;
    }

    public async Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync(string date)
    {
      var result = await _bancoCentralAPI.GetExchangeRateAsync(date);
      return result.AsDomainModel();
    }
    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date)
    {
      var result = (await _bancoCentralAPI.GetOpeningRateAsync(date)).Value.Where(x => x.tipoBoletim.ToLower().Equals("fechamento ptax"));
      if (result == null) {
        return Enumerable.Empty<CommodityOpenHighLowClose>();
      }
      return result.AsOHLCDomainModel();
    }
  }
}
