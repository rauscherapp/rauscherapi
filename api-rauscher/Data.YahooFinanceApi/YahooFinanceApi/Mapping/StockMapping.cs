using Data.YahooFinanceApi.Api.Model;
using Domain.Models;


namespace Data.Commodities.Api.Mapping
{
  public static class StockMapping
  {
    public static IEnumerable<CommoditiesRate> AsDomainModel(this QuoteResponse models)
    {
      if (models?.QuoteResponseData.Results == null)
      {
        return Enumerable.Empty<CommoditiesRate>();
      }

      return models.QuoteResponseData.Results.Select(model => model.AsDomainModel());
    }

    public static CommoditiesRate AsDomainModel(this Result model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      var unit = string.Empty;
      // Uncomment and implement this if there is logic to determine the unit
      // if (model?.Count > 0)
      //{
      //  unit = model.FirstOrDefault(x => x.== model.Key).Value ?? "Unknown Unit";
      //}

      decimal variationPrice = 0m;
      decimal variationPercent = 0m;
      bool someBooleanFlag = false;
      var regularMarketTime = DateTimeOffset.FromUnixTimeSeconds(model.RegularMarketTime).DateTime;

      return new CommoditiesRate(
          model.RegularMarketTime, // Assuming this should be a DateTime conversion
          "USD",
          regularMarketTime,
          model.Symbol,
          unit,
          (decimal)model.RegularMarketPrice,
          (decimal)model.RegularMarketChange,
          (decimal)model.RegularMarketChangePercent,
          model.isUp()
          );
    }

    public static bool isUp(this Result value)
    {
      return value.RegularMarketChange < 0;
    }
  }
}
