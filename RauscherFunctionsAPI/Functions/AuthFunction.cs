using APIs.Security.JWT;
using Application.Interfaces;
using Application.ViewModels;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{
  public class AuthFunctions : BaseFunctions
  {
    private readonly IAuthService _authService;
    private readonly IMediatorHandler _bus;

    public AuthFunctions(
        IAuthService authService,
        IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications)
        : base(notifications, bus)
    {
      _authService = authService;
      _bus = bus;
    }

    [FunctionName("RegisterUser")]
    public async Task<IActionResult> RegisterUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/auth/register")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request to register user.");

      try
      {
        var parseResult = await ParseUserRequestAsync(req);
        if (parseResult.ErrorResult != null)
        {
          return parseResult.ErrorResult;
        }

        var validationErrors = ValidateAuthRequest(parseResult.Request!);
        if (validationErrors != null)
        {
          return CreateErrorResponse(
              StatusCodes.Status400BadRequest,
              "Os dados informados são inválidos.",
              validationErrors);
        }

        var userRequest = parseResult.Request!;
        var result = await _authService.Register(userRequest);

        if (!result.IsValid || result.Token == null)
        {
          return CreateErrorResponse(
              StatusCodes.Status400BadRequest,
              "Os dados informados são inválidos.");
        }

        return new ObjectResult(result.Token)
        {
          StatusCode = StatusCodes.Status201Created
        };
      }
      catch (DuplicateUserException ex)
      {
        return CreateErrorResponse(StatusCodes.Status409Conflict, ex.Message);
      }
      catch (Exception ex)
      {
        log.LogError($"Error registering user: {ex.Message}");
        return CreateErrorResponse(
            StatusCodes.Status500InternalServerError,
            "Ocorreu um erro interno ao processar a requisição.");
      }
    }

    [FunctionName("LoginUser")]
    public async Task<IActionResult> LoginUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/auth/login")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request for user login.");

      try
      {
        var parseResult = await ParseUserRequestAsync(req);
        if (parseResult.ErrorResult != null)
        {
          return parseResult.ErrorResult;
        }

        var validationErrors = ValidateAuthRequest(parseResult.Request!);
        if (validationErrors != null)
        {
          return CreateErrorResponse(
              StatusCodes.Status400BadRequest,
              "Os dados informados são inválidos.",
              validationErrors);
        }

        var userRequest = parseResult.Request!;
        var result = await _authService.AppLogin(userRequest);

        if (!result.IsValid || result.Token == null)
        {
          return CreateErrorResponse(
              StatusCodes.Status401Unauthorized,
              "E-mail ou senha inválidos.");
        }

        return new OkObjectResult(result.Token);
      }
      catch (Exception ex)
      {
        log.LogError($"Error logging in user: {ex.Message}");
        return CreateErrorResponse(
            StatusCodes.Status500InternalServerError,
            "Ocorreu um erro interno ao processar a requisição.");
      }
    }

    private static async Task<(UserRequest Request, IActionResult ErrorResult)> ParseUserRequestAsync(HttpRequest req)
    {
      try
      {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          return (new UserRequest(), null);
        }

        var userRequest = JsonSerializer.Deserialize<UserRequest>(
            requestBody,
            new JsonSerializerOptions
            {
              PropertyNameCaseInsensitive = true
            });

        return (userRequest ?? new UserRequest(), null);
      }
      catch (JsonException)
      {
        return (null, CreateErrorResponse(
            StatusCodes.Status400BadRequest,
            "O corpo da requisição está inválido."));
      }
    }

    private static IDictionary<string, string[]> ValidateAuthRequest(UserRequest request)
    {
      var errors = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

      if (string.IsNullOrWhiteSpace(request.Email))
      {
        errors["email"] = new List<string> { "O campo email é obrigatório." };
      }
      else if (!new EmailAddressAttribute().IsValid(request.Email))
      {
        errors["email"] = new List<string> { "O campo email deve ser um endereço de e-mail válido." };
      }

      if (string.IsNullOrWhiteSpace(request.Password))
      {
        errors["password"] = new List<string> { "O campo password é obrigatório." };
      }

      if (errors.Count == 0)
      {
        return null;
      }

      var normalizedErrors = new Dictionary<string, string[]>();
      foreach (var error in errors)
      {
        normalizedErrors[error.Key] = error.Value.ToArray();
      }

      return normalizedErrors;
    }

    private static IActionResult CreateErrorResponse(
        int statusCode,
        string message,
        IDictionary<string, string[]> errors = null)
    {
      return new ObjectResult(new
      {
        success = false,
        message,
        errors
      })
      {
        StatusCode = statusCode
      };
    }

    [FunctionName("CheckUserSubscription")]
    public async Task<IActionResult> CheckUserSubscription(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/auth/checkSubscription")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request to check user subscription.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var userRequest = JsonSerializer.Deserialize<UserRequest>(requestBody);

      if (userRequest == null)
      {
        return CreateResponse(new { message = "Invalid request body." });
      }

      try
      {
        var hasSubscription = new HasValidSignatureViewModel
        {
          HasValidSignature = await _authService.CheckSubscription(userRequest)
        };

        return CreateResponse(hasSubscription);
      }
      catch (Exception ex)
      {
        log.LogError($"Error checking subscription: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("DeleteAccount")]
    public async Task<IActionResult> DeleteAccount(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/auth/deleteAccount")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing account deletion request.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var request = JsonSerializer.Deserialize<UserRequest>(requestBody);

      if (request == null || string.IsNullOrEmpty(request.Email))
      {
        return CreateResponse(new { message = "Invalid request. Email is required." });
      }

      try
      {
        var success = await _authService.DeleteAccount(request.Email);

        if (success)
        {
          return CreateResponse(new { message = "Account successfully deleted." });
        }
        return CreateResponse(new { message = "Account not found or could not be deleted." });
      }
      catch (Exception ex)
      {
        log.LogError($"Error deleting account: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
