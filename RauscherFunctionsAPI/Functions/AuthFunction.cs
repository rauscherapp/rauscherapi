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
using System.IO;
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

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var userRequest = JsonSerializer.Deserialize<UserRequest>(requestBody);

      if (userRequest == null)
      {
        return CreateResponse(new { message = "Invalid request body." });
      }

      try
      {
        var result = await _authService.Register(userRequest);
        if (!result.IsValid)
        {
          return new BadRequestObjectResult(new
          {
            success = false,
            errors = new[] { "Could not create the account." }
          });
        }
        return new OkObjectResult(result.Token);
      }
      catch (Exception ex)
      {
        log.LogError($"Error registering user: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("LoginUser")]
    public async Task<IActionResult> LoginUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/auth/login")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request for user login.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var userRequest = JsonSerializer.Deserialize<UserRequest>(requestBody);

      if (userRequest == null)
      {
        return CreateResponse(new { message = "Invalid request body." });
      }

      try
      {
        var result = await _authService.AppLogin(userRequest);
        return new OkObjectResult(result.Token);
      }
      catch (Exception ex)
      {
        log.LogError($"Error logging in user: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
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

      if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
      {
        return new BadRequestObjectResult(new
        {
          success = false,
          errors = new[] { "Email and password are required." }
        });
      }

      try
      {
        var success = await _authService.DeleteAccount(request);

        if (success)
        {
          return CreateResponse(new { message = "Account successfully deleted." });
        }
        // Mesma resposta para conta inexistente e senha errada, para não revelar quais e-mails existem
        return new UnauthorizedObjectResult(new
        {
          success = false,
          errors = new[] { "Invalid credentials." }
        });
      }
      catch (Exception ex)
      {
        log.LogError($"Error deleting account: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
