using Data.BancoCentral.Api.Model;
using System.Globalization;

namespace Data.BancoCentral.Api.Mapping
{
  public static class OHLCMapping
  {
    public static IEnumerable<CommodityOpenHighLowClose> AsOHLCDomainModel(this IEnumerable<Value> models)
    {
      if (models == null)
      {
        return Enumerable.Empty<CommodityOpenHighLowClose>();
      }

      return models.Select(model => model.AsOHLCDomainModel());
    }
    public static CommodityOpenHighLowClose AsOHLCDomainModel(this Value model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      DateTimeOffset date;
      if (!DateTimeOffset.TryParse(model.dataHoraCotacao, out date))
      {
        throw new FormatException($"Invalid date format: {model.dataHoraCotacao}");
      }

      return new CommodityOpenHighLowClose
      {
        Base = "USD",
        Date = date.DateTime,
        PriceClose = model.cotacaoVenda.ParseDecimal(),
        Symbol = "PTAX",
        Timestamp = date.ToUnixTimeSeconds()
      };
    }
    public static decimal ParseDecimal(this string value)
    {
      if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalModel) && decimalModel != 0)
      {
        return decimalModel;
      }
      return 0m;
    }
  }
}
