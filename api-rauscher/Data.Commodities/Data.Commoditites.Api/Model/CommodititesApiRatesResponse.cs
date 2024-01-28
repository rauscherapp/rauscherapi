using System.Text.Json.Serialization;

public class ApiResponseWrapper
{
  [JsonPropertyName("data")]
  public CommoditiesApiRatesResponse Data { get; set; }
}

public class CommoditiesApiRatesResponse
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
  [JsonPropertyName("unit")]
  public Dictionary<string, string>? Unit { get; set; }
}
