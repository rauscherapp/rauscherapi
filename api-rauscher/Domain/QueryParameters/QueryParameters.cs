using System;
using System.Text.Json.Serialization;

namespace Domain.QueryParameters
{
  public class QueryParameters
  {

    const int maxPageSize = 50;
    [JsonPropertyName("searchQuery")]
    public string SearchQuery { get; set; }
    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; } = 0;
    [JsonPropertyName("orderBy")]
    public string OrderBy { get; set; } = "";
    [JsonPropertyName("fields")]
    public string Fields { get; set; }
    
    private int _pageSize = 10;
    [JsonPropertyName("pageSize")]
    public int PageSize
    {
      get => _pageSize;
      set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }

  }
}
