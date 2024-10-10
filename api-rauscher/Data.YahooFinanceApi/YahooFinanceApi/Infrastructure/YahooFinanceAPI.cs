using Data.YahooFinanceApi.Api.Interfaces;
using Data.YahooFinanceApi.Api.Model;
using Data.YahooFinanceApi.Api.Options;
using Domain.Models;
using Domain.Options;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.YahooFinanceApi.Api.Infrastructure
{
  public class YahooFinanceAPI : IYahooFinanceAPI
  {
    private readonly HttpClient _client;
    private readonly string _baseUrl;
    private readonly string _symbols;
    private readonly string _apiKey;
    public YahooFinanceAPI(HttpClient client, IOptions<YahooFinanceOptions> options, IOptionsSnapshot<ParametersOptions> parameters)
    {
      _client = client ?? throw new ArgumentNullException(nameof(client));
      _baseUrl = options?.Value?.BaseUrl ?? throw new ArgumentNullException(nameof(options));
      _symbols = options?.Value?.Symbols ?? throw new ArgumentNullException(nameof(options));
      _apiKey = parameters.Value.YahooFinanceApiKey;
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

    public async Task<QuoteResponse> GetExchangeRateAsync()
    {
      string endpoint = $"quote?region=BR&lang=pt-BR&symbols={_symbols}";
      return await GetAsync<QuoteResponse>(endpoint);
    }
    public async Task<QuoteResponse> GetExchangeRateAsync(string code)
    {
      string endpoint = $"quote?region=BR&lang=pt-BR&symbols={code}";
      return await GetAsync<QuoteResponse>(endpoint);
    }
  }
}
