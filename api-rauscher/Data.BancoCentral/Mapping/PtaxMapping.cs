using Data.BancoCentral.Api.Model;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Commodities.Api.Mapping
{
  public static class PtaxMapping
  {
    public static IEnumerable<CommoditiesRate> AsDomainModel(this ExchangeRate models)
    {
      if (models?.Value == null)
      {
        return Enumerable.Empty<CommoditiesRate>();
      }

      return models.Value.Select(model => model.AsDomainModel());
    }

    public static CommoditiesRate AsDomainModel(this Value model)
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

      return new CommoditiesRate(
          Convert.ToDateTime(model.dataHoraCotacao).ToUnixTimestamp(), // Assuming this should be a DateTime conversion
          "",
          Convert.ToDateTime(model.dataHoraCotacao),
          "PTAX",
          unit,
          model.cotacaoVenda.CalculatePrice(),
          variationPrice,
          variationPercent,
          someBooleanFlag);
    }

    public static decimal CalculatePrice(this string value)
    {
      if (decimal.TryParse(value, out decimal decimalModel) && decimalModel != 0)
      {
        decimalModel /= 100000;
        return decimalModel;
      }
      return 0m;
    }

    private static long ToUnixTimestamp(this DateTime dateTime)
    {
      DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime);
      return dateTimeOffset.ToUnixTimeSeconds();
    }
  }
}
