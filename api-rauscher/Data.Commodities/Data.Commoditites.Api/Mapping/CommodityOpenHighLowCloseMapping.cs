using Data.Commodities.Api.Model;

namespace Data.Commodities.Api.Mapping
{
  public static class CommodityOpenHighLowCloseMapper
  {
    // Method to map OpenHighLowCloseResponse to a list of CommodityOpenHighLowClose
    public static IEnumerable<CommodityOpenHighLowClose> OhlcAsDomainModel(this OpenHighLowCloseResponse model)
    {
      if (model == null || model.Rates == null || !model.Rates.Any())
      {
        return Enumerable.Empty<CommodityOpenHighLowClose>();
      }

      // Convert the rates dictionary to a CommodityOpenHighLowClose list
      return model.Rates.Select(rate => model.MapToDomainModel(rate.Key, rate.Value)).ToList();
    }

    // Helper method to convert a single rate entry to CommodityOpenHighLowClose
    private static CommodityOpenHighLowClose MapToDomainModel(this OpenHighLowCloseResponse model, string rateType, double rateValue)
    {
      // Map rate values to the domain entity, assuming the rateType is a meaningful indicator for your domain model
      return new CommodityOpenHighLowClose(
          model.Timestamp,
          model.Date,
          model.Base,
          model.Symbol,
          model.Rates.GetRate("open"),
          model.Rates.GetRate("high"),
          model.Rates.GetRate("low"),
          model.Rates.GetRate("close")
      );
    }

    // Helper method to get the rate safely from the dictionary
    public static decimal GetRate(this Dictionary<string, double> rates, string rateType)
    {
      if (rates.TryGetValue(rateType, out double rateValue))
      {
        // Convert to decimal
        return 1 / Convert.ToDecimal(rateValue);
      }

      // Return 0 if the rate type is not found
      return 0m;
    }
  }
}
