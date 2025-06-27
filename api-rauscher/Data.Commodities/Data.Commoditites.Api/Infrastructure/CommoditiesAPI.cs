using Data.Commodities.Api.Interfaces;
using Data.Commodities.Api.Model;
using Data.Commoditites.Api.Options;
using Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System.Text.Json;

namespace Data.Commodities.Api.Infrastructure
{
  public class CommoditiesAPI : ICommoditiesAPI
  {
    private readonly HttpClient _httpClient;
    private readonly ILogger<CommoditiesAPI> _logger;
    private readonly string _apiKey;

    public CommoditiesAPI(HttpClient httpClient,
                                 ILogger<CommoditiesAPI> logger,
                                 IOptions<CommoditiesApiOptions> options,
                                 IOptionsSnapshot<ParametersOptions> parameters)
    {
      _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _apiKey = parameters.Value.CommoditiesApiKey;
      _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);

    }


    public async Task<OpenHighLowCloseResponse> GetOHLCAsync(string symbol)
    {
      try
      {
        var url = $"open-high-low-close/{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}?access_key={_apiKey}&base=USD&symbols={symbol}";
        var response = await Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(() => _httpClient.GetAsync(url));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenHighLowCloseResponse>(content);
        if (result != null && result != null) { return result; }
        else return null;

      }
      catch (HttpRequestException e)
      {
        _logger.LogError(e, "Error occurred while calling Commodities API");
        throw;
      }
      catch (JsonException e)
      {
        _logger.LogError(e, "Error occurred while deserializing Commodities API response");
        throw;
      }
    }

    public async Task<Dictionary<string, string>> GetSymbolsAsync()
    {
      try
      {
        var url = $"symbols?access_key={_apiKey}";
        var response = await Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(() => _httpClient.GetAsync(url));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
        if (result != null && result != null) { return result; }
        else return null;

      }
      catch (HttpRequestException e)
      {
        _logger.LogError(e, "Error occurred while calling Commodities API");
        throw;
      }
      catch (JsonException e)
      {
        _logger.LogError(e, "Error occurred while deserializing Commodities API response");
        throw;
      }
    }

    public async Task<ApiResponseWrapper> GetExchangeRateAsync(string symbols, string baseCurrency = "USD")
    {
      try
      {
        var url = $"latest?access_key={_apiKey}";
        string symbolsQueryParam = string.Join(",", symbols);
        string queryParams = $"&base={baseCurrency}&symbols={symbolsQueryParam}";
        var response = await Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(() => _httpClient.GetAsync(url + queryParams));

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ApiResponseWrapper>(content);
        if (result.Data.Base.Contains("USD"))
        {
          // Usar LINQ para filtrar as chaves que não começam com "USD" e removê-las
          foreach (var key in result.Data.Rates.Where(key => key.Key.StartsWith("USD")).ToList())
          {
              result.Data.Rates.Remove(key.Key);
          }
        }

        if (result != null && result != null) { return result; }
        else return null;
      }
      catch (HttpRequestException e)
      {
        _logger.LogError(e, "Error occurred while calling Commodities API");
        throw;
      }
      catch (JsonException e)
      {
        _logger.LogError(e, "Error occurred while deserializing Commodities API response");
        throw;
      }
    }
  }
}
