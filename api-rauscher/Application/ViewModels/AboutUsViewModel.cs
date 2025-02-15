using System;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
  public class AboutUsViewModel
  {
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
  }
}
