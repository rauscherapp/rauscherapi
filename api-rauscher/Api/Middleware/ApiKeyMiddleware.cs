using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ApiKeyMiddleware> _logger;
  private readonly IConfiguration _configuration;
  private const string ApiKeyHeaderName = "ApiKey";

  public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger, IConfiguration configuration)
  {
    _next = next;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
      {
        _logger.LogWarning("API Key was not provided.");
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("API Key is missing.");
        return;
      }

      var apiKey = _configuration["ApiKey"];

      if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(extractedApiKey))
      {
        _logger.LogWarning("Unauthorized client: Invalid API Key.");
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("Unauthorized client.");
        return;
      }

      // Proceed to the next middleware
      await _next(context);
    }
    catch (Exception ex)
    {
      _logger.LogError($"An error occurred in {nameof(ApiKeyMiddleware)}: {ex.Message}");
      context.Response.StatusCode = 500; // Internal Server Error
      await context.Response.WriteAsync("An internal server error occurred.");
    }
  }
}
