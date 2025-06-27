using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RauscherFunctionsAPI;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class EmailFunction : BaseFunctions
{
  private readonly IEmailService _emailService;
  private readonly IMediatorHandler _bus;

  public EmailFunction(
      IEmailService emailService,
      IMediatorHandler bus,
      INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
  {
    _emailService = emailService;
    _bus = bus;
  }

  [FunctionName("SendEmail")]
  public async Task<IActionResult> SendEmail(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Email/SendEmail")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing request to send email.");

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var emailRequest = JsonSerializer.Deserialize<AppEmailParameters>(requestBody);

    if (emailRequest == null || !IsValidOperation())
    {
      //NotifyModelStateErrors();
      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }

    try
    {
      await _emailService.SendEmailAsync(emailRequest);
      return new OkObjectResult(new { success = true, message = "Email sent successfully." });
    }
    catch (Exception ex)
    {
      log.LogError($"Error sending email: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }
}
