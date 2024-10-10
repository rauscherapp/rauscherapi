using Data.Commodities.Api.Interfaces;
using Data.Commodities.Api.Mapping;
using Domain.Models;
using Domain.Repositories;

namespace Data.Commodities.Api.Service
{
  public class TradeReadRepository : Repository, ITradeReadRepository
  {

    private readonly ICommoditiesAPI _commoditiesAPI;
    public TradeReadRepository(ICommoditiesAPI commoditiesAPI)
    {
      _commoditiesAPI = commoditiesAPI;
    }

    public async Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync(string codes)
    {
      var result = await _commoditiesAPI.GetExchangeRateAsync(codes);
      return result.AsDomainModel();
    }
    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string codes)
    {
      var result = await _commoditiesAPI.GetOHLCAsync(codes);
      if (result == null)
      {
        return Enumerable.Empty<CommodityOpenHighLowClose>();
      }
      return result.OhlcAsDomainModel();
    }
  }
}
