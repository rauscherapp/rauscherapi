using Data.YahooFinanceApi.Api.Model;

namespace Data.YahooFinanceApi.Api.Mapping
{
  public static class OHLCMapping
  {
    public static IEnumerable<CommodityOpenHighLowClose> AsOHLCDomainModel(this QuoteResponse models)
    {
      if (models?.QuoteResponseData.Results == null)
      {
        return Enumerable.Empty<CommodityOpenHighLowClose>();
      }

      return models.QuoteResponseData.Results.Select(model => model.AsOHLCDomainModel());
    }
    public static CommodityOpenHighLowClose AsOHLCDomainModel(this Result model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }
      return new CommodityOpenHighLowClose
      {
        Base = model.Currency,
        Symbol = model.Symbol,
        Date = DateTime.UtcNow,
        PriceOpen = model.RegularMarketOpen.ParseDecimal(),
        PriceHigh = model.RegularMarketDayHigh.ParseDecimal(),
        PriceLow = model.RegularMarketDayLow.ParseDecimal(),
        PriceClose = model.RegularMarketPreviousClose.ParseDecimal(),
      };
    }
    public static decimal ParseDecimal(this double value)
    {
      // Converte o valor de double para decimal
      decimal decimalValue = (decimal)value;

      // Verifica se o valor convertido não é zero
      if (decimalValue != 0)
      {
        return decimalValue;
      }

      // Retorna 0m se o valor convertido for zero
      return 0m;
    }
  }
}
