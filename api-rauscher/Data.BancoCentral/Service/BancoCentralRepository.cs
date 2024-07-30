using Data.BancoCentral.Api.Model;
using Data.BancoCentral.Api.Options;
using Data.Commodities.Api.Mapping;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Data.BancoCentral.Api.Service
{
  public class BancoCentralRepository : IBancoCentralRepository
  {
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    public BancoCentralRepository(HttpClient client, IOptions<BancoCentralOptions> options)
    {
      _client = client ?? throw new ArgumentNullException(nameof(client));
      _baseUrl = options?.Value?.BaseUrl ?? throw new ArgumentNullException(nameof(options));
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

    public async Task<IEnumerable<CommoditiesRate>> GetExchangeRateAsync(string date)
    {
      string endpoint = $"odata/CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{date}'&$format=json";
      var exchangeRate = await GetAsync<ExchangeRate>(endpoint);
      return exchangeRate.AsDomainModel();
    }

    public async Task<ExchangeRate> GetLatestExchangeRateAsync()
    {
      string endpoint = "odata/CotacaoDolarUltimoDia($top=1&$orderby=dataHoraCotacao%20desc)&$format=json";
      return await GetAsync<ExchangeRate>(endpoint);
    }

    public async Task<ExchangeRates> GetExchangeRatesByPeriodAsync(string startDate, string endDate)
    {
      string endpoint = $"odata/CotacaoDolarPeriodo(dataInicial=@dataInicial,dataFinalCotacao=@dataFinalCotacao)?@dataInicial='{startDate}'&@dataFinalCotacao='{endDate}'&$format=json";
      return await GetAsync<ExchangeRates>(endpoint);
    }

    public async Task<Currencies> GetAllCurrenciesAsync()
    {
      string endpoint = "odata/Moedas?$format=json";
      return await GetAsync<Currencies>(endpoint);
    }
  }
}
