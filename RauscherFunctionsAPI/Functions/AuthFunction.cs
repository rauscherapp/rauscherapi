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

      var result = await _authService.Register(userRequest);

      return new OkObjectResult(result.Token);
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

      var result = await _authService.AppLogin(userRequest);

      return new OkObjectResult(result.Token);
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

      var hasSubscription = new HasValidSignatureViewModel
      {
        HasValidSignature = await _authService.CheckSubscription(userRequest)
      };

      return CreateResponse(hasSubscription);
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

      var success = await _authService.DeleteAccount(request.Email);

      if (success)
      {
        return CreateResponse(new { message = "Account successfully deleted." });
      }
      return CreateResponse(new { message = "Account not found or could not be deleted." });
    }
  }
}
