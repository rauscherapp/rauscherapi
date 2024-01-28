using Data.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class ApiKeyMiddleware
{
  private readonly RequestDelegate _next;

  public ApiKeyMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context, RauscherDbContext dbContext)
  {

    if (!context.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey))
    {
      context.Response.StatusCode = 401; // Unauthorized
      return;
    }

    var apiCredential = dbContext.ApiCredentialss.FirstOrDefault(c => c.Apikey == extractedApiKey.ToString());

    if (apiCredential == null || !IsValidSecret(apiCredential.Apisecrethash, context))
    {
      context.Response.StatusCode = 401; // Unauthorized
      return;
    }

    await _next(context);
  }

  private bool IsValidSecret(string storedSecretHash, HttpContext context)
  {
    // Extract the secret from the request header
    if (!context.Request.Headers.TryGetValue("ApiSecret", out var providedSecret))
    {
      return false;
    }

    // Hash the provided secret
    using (var sha256 = SHA256.Create())
    {
      var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(providedSecret));
      var hashedSecret = BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();

      // Compare the hashed secret with the stored hash
      return hashedSecret == storedSecretHash;
    }
  }
}
