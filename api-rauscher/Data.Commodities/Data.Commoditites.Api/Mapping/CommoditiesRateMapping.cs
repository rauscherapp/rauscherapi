using Domain.Models;
using System.Linq;

namespace Data.Commoditites.Api.Mapping
{
  public static class CommoditiesRateMapping
  {
    public static IEnumerable<CommoditiesRate> AsDomainModel(this ApiResponseWrapper models)
    {
      return models.Data.Rates.Select(model => model.AsDomainModel(models.Data));
    }

    public static CommoditiesRate AsDomainModel(this KeyValuePair<string, double> model, CommoditiesApiRatesResponse response)
    {
      var unit = string.Empty;
      if (response?.Unit?.Count > 0)
      {
        unit = response.Unit.FirstOrDefault(x => x.Key == model.Key).Value ?? "Unknown Unit";
      }
      
      decimal variationPrice = 0m;
      decimal variationPercent = 0m;
      bool someBooleanFlag = false;

      return new CommoditiesRate(
          response.Timestamp,
          response.Base,
          response.Date,
          model.Key,
          unit,
          model.Value.CalculatePrice(),
          variationPrice,
          variationPercent,
          someBooleanFlag);
    }    
    public static decimal CalculatePrice(this double model)
    {
      decimal decimalModel = Convert.ToDecimal(model);
      if (decimalModel == 0)
      {
        return 0m;
      }
      return 1M / decimalModel;
    }
  }
}
