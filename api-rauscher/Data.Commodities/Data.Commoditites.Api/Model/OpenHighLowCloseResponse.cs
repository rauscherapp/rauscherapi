using System.Text.Json.Serialization;

namespace Data.Commodities.Api.Model
{
  public class OpenHighLowCloseResponse
  {
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; }
    [JsonPropertyName("base")]
    public string Base { get; set; }
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }
    [JsonPropertyName("rates")]
    public Dictionary<string, double>? Rates { get; set; }
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
  }
}
