using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
  public class CommoditiesRateViewModel
  {
    [JsonPropertyName("Id")]
    public Guid Id { get; set; }
    [JsonPropertyName("Timestamp")]
    public long? Timestamp { get; set; }
    [JsonPropertyName("Base")]
    public string Base { get; set; }
    [JsonPropertyName("Date")]
    public DateTime? Date { get; set; }
    [JsonPropertyName("Code")]
    public string Code { get; set; }
    [JsonPropertyName("Unit")]
    public string Unit { get; set; }
    [JsonPropertyName("Price")]
    public decimal? Price { get; set; }
    [JsonPropertyName("FormattedPrice")]
    public string FormattedPrice { get; set; }
    [JsonPropertyName("Variationprice")]
    public decimal? Variationprice { get; set; }
    [JsonPropertyName("Variationpricepercent")]
    public decimal? Variationpricepercent { get; set; }
    [JsonPropertyName("FullVariationPricePercent")]
    public string FullVariationPricePercent { get; set; }
    [JsonPropertyName("TimestampDate")]
    public string TimestampDate { get; set; }
    [JsonPropertyName("Isup")]
    public Boolean Isup { get; set; }
  }
}
