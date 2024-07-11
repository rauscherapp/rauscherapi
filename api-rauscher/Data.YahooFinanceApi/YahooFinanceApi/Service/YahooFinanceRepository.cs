using Data.YahooFinanceApi.Api.Model;
using Data.YahooFinanceApi.Api.Options;
using Data.Commodities.Api.Mapping;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Data.YahooFinanceApi.Api.Service
{
  public class YahooFinanceRepository : IYahooFinanceRepository
  {
    private readonly HttpClient _client;
    private readonly string _baseUrl;
    private readonly string _symbols;
    private readonly string _apiKey;
    public YahooFinanceRepository(HttpClient client, IOptions<YahooFinanceOptions> options)
    {
      _client = client ?? throw new ArgumentNullException(nameof(client));
      _baseUrl = options?.Value?.BaseUrl ?? throw new ArgumentNullException(nameof(options));
      _symbols = options?.Value?.Symbols ?? throw new ArgumentNullException(nameof(options));
      _apiKey = options?.Value?.ApiKey ?? throw new ArgumentNullException(nameof(options));
      _client.DefaultRequestHeaders.Add("x-api-key", _apiKey);
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
      try
      {
        
        HttpResponseMessage response = await _client.GetAsync(_baseUrl + endpoint);
        response.EnsureSuccessStatusCode();
        string responseData = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseData);
      }
      catch (HttpRequestException ex)
      {
        // Handle HTTP request exceptions
        Console.WriteLine($"Request error: {ex.Message}");
        throw;
      }
      catch (Exception ex)
      {
        // Handle other exceptions (e.g., serialization issues)
        Console.WriteLine($"Error: {ex.Message}");
        throw;
      }
    }

    public async Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync()
    {
      string endpoint = $"quote?region=BR&lang=pt-BR&symbols={_symbols}";
      var exchangeRate = await GetAsync<QuoteResponse>(endpoint);
      return exchangeRate.AsDomainModel();
    }
  }
}
