using Data.Commodities.Api.Mapping;
using Data.YahooFinanceApi.Api.Interfaces;
using Data.YahooFinanceApi.Api.Mapping;
using Domain.Models;
using Domain.Repositories;

namespace Data.YahooFinanceApi.Api.Service
{
  public class TradeReadRepository : Repository, ITradeReadRepository
  {

    private readonly IYahooFinanceAPI _yahooFinanceAPI;
    public TradeReadRepository(IYahooFinanceAPI yahooFinanceAPI)
    {
      _yahooFinanceAPI = yahooFinanceAPI;
    }

    public async Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync(string symbols)
    {
      var result = await _yahooFinanceAPI.GetExchangeRateAsync(symbols);
      return result.AsDomainModel();
    }
    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string symbol)
    {
      var result = (await _yahooFinanceAPI.GetExchangeRateAsync(symbol));
      if (result == null)
      {
        return Enumerable.Empty<CommodityOpenHighLowClose>();
      }
      return result.AsOHLCDomainModel();
    }
  }
}
