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
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{
  public class ApiSecretsFunction : BaseFunctions
  {
    private readonly IApiCredentialsAppService _apiCredentialsAppService;
    private readonly IMediatorHandler _bus;

    public ApiSecretsFunction(IApiCredentialsAppService apiCredentialsAppService, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        : base(notifications, bus)
    {
      _apiCredentialsAppService = apiCredentialsAppService;
      _bus = bus;
    }

    [FunctionName("PostSecret")]
    public async Task<IActionResult> Secret(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Secrets")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request for Secrets.");

      // Read query parameters
      var document = req.Query["document"];

      try
      {
        var result = await _apiCredentialsAppService.GerarApiCredentials(document);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error generating API credentials: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
