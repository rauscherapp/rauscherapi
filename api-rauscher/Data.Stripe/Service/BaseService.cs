//using Domain.Options;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using StripeApi.Options;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;

//namespace StripeApi.Service
//{
//  public class StripeService
//  {
//    private readonly HttpClient _httpClient;
//    private readonly ILogger<StripeService> _logger;
//    private readonly ParametersOptions _apiKey;

//    public StripeService(HttpClient httpClient, ILogger<StripeService> logger, IOptionsSnapshot<ParametersOptions> parameters)
//    {
//      _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
//      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//      _apiKey = parameters.Value;

//    //  _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
//      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey.StripeApiSecret);
//    }

//    public async Task<T> PostAsync<T>(string endpoint, object data)
//    {
//      try
//      {
//        _logger.LogInformation($"Post Call of: {nameof(T)}");
//        var json = JsonSerializer.Serialize(data);
//        var content = new StringContent(json, Encoding.UTF8, "application/json");
//        var response = await _httpClient.PostAsync(endpoint, content);

//        response.EnsureSuccessStatusCode();

//        var responseData = await response.Content.ReadAsStringAsync();
//        return JsonSerializer.Deserialize<T>(responseData);
//      }
//      catch (HttpRequestException ex)
//      {
//        _logger.LogError(ex, $"Post Call Exception for: {nameof(T)} HTTP Request Error: {ex.Message}");
//        throw;
//      }
//      catch (JsonException ex)
//      {
//        _logger.LogError(ex, $"Post Call Exception for: {nameof(T)} HTTP Request Error: {ex.Message}");
//        throw;
//      }
//    }

//    public async Task<T> GetAsync<T>(string endpoint)
//    {
//      try
//      {
//        _logger.LogInformation($"Getting Call of: {nameof(T)}");
//        var response = await _httpClient.GetAsync(endpoint);

//        response.EnsureSuccessStatusCode();

//        var responseData = await response.Content.ReadAsStringAsync();
//        return JsonSerializer.Deserialize<T>(responseData);
//      }
//      catch (HttpRequestException ex)
//      {
//        _logger.LogError(ex, $"Post Call Exception for: {nameof(T)} HTTP Request Error: {ex.Message}");
//        throw;
//      }
//      catch (JsonException ex)
//      {
//        _logger.LogError(ex, $"Post Call Exception for: {nameof(T)} HTTP Request Error: {ex.Message}");
//        throw;
//      }
//    }
//  }
//}
