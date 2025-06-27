using Application.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;

public class StripeListenerFunction
{
  private readonly string _webhookSecret;
  private readonly IAuthService _authService;

  public StripeListenerFunction(IOptionsSnapshot<ParametersOptions> parameters, IAuthService authService)
  {
    _webhookSecret = parameters.Value.StripeWebhookSecret;
    _authService = authService;
  }

  [FunctionName("StripeWebhook")]
  public async Task<IActionResult> Webhook(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Stripe/webhook")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing Stripe webhook.");

    var json = await new StreamReader(req.Body).ReadToEndAsync();
    Event stripeEvent;

    try
    {
      stripeEvent = EventUtility.ConstructEvent(
          json,
          req.Headers["Stripe-Signature"],
          _webhookSecret
      );
      log.LogInformation($"Webhook notification with type: {stripeEvent.Type} received for {stripeEvent.Id}");
    }
    catch (Exception e)
    {
      log.LogError($"Failed to process webhook: {e.Message}");
      return new BadRequestResult();
    }

    if (stripeEvent.Type == "customer.subscription.deleted")
    {
      var session = stripeEvent.Data.Object as Stripe.Subscription;
      await _authService.CancelledSubscriptionUserUpdate(session.CustomerId);
      log.LogInformation($"Processed customer.subscription.deleted event for Customer ID: {session.CustomerId}");
    }

    if (stripeEvent.Type == "customer.subscription.created")
    {
      var session = stripeEvent.Data.Object as Stripe.Subscription;
      await _authService.SuccesfullSubscriptionUserUpdate(session.CustomerId);
      log.LogInformation($"Processed customer.subscription.created event for Customer ID: {session.CustomerId}");
    }

    return new OkResult();
  }
}
