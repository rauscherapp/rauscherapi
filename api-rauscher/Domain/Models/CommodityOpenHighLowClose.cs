using System;

public class CommodityOpenHighLowClose
{
  public CommodityOpenHighLowClose() { }
  public CommodityOpenHighLowClose(long timestamp, DateTime date, string @base, string symbol, decimal open, decimal high, decimal low, decimal close)
  {
    Id = Guid.NewGuid();
    Timestamp = timestamp;
    Date = date;
    Base = @base;
    Symbol = symbol;
    PriceOpen = open;
    PriceHigh = high;
    PriceLow = low;
    PriceClose = close;
  }

  public Guid Id { get; set; }
  public long Timestamp { get; set; }
  public DateTime Date { get; set; }
  public string Base { get; set; }
  public string Symbol { get; set; }
  public decimal PriceOpen { get; set; }
  public decimal PriceHigh { get; set; }
  public decimal PriceLow { get; set; }
  public decimal PriceClose { get; set; }
}
