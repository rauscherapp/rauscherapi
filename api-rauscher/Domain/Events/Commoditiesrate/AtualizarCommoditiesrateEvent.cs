using Domain.Core.Events;
using System;

namespace Domain.Events
{
  public class AtualizarCommoditiesRateEvent : Event
  {
    public AtualizarCommoditiesRateEvent(
    Guid id,
    long? timestamp,
    string baseCurrency,
    DateTime? date,
    string code,
    string unit,
    decimal? price,
    decimal? variationprice,
    decimal? variationpricepercent,
    Boolean isup
    )
    {
      Id = id;
      Timestamp = timestamp;
      BaseCurrency = baseCurrency;
      Date = date;
      Code = code;
      Unit = unit;
      Price = price;
      Variationprice = variationprice;
      Variationpricepercent = variationpricepercent;
      Isup = isup;
    }
    public Guid Id { get; set; }
    public long? Timestamp { get; set; }
    public string BaseCurrency { get; set; }
    public DateTime? Date { get; set; }
    public string Code { get; set; }
    public string Unit { get; set; }
    public decimal? Price { get; set; }
    public decimal? Variationprice { get; set; }
    public decimal? Variationpricepercent { get; set; }
    public Boolean Isup { get; set; }
  }
}
