using Application.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Api.Controllers.Api
{
  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [AllowAnonymous]
  public class StripeListenerController : Controller
  {
    private readonly string _webhookSecret;
    private readonly IAuthService _authService;

    public StripeListenerController(IOptionsSnapshot<ParametersOptions> parameters, IAuthService authService)
    {
      _webhookSecret = parameters.Value.StripeWebhookSecret;
      _authService = authService;
    }
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
      var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
      Event stripeEvent;
      try
      {
        stripeEvent = EventUtility.ConstructEvent(
            json,
            Request.Headers["Stripe-Signature"],
            _webhookSecret
        );
        Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
      }
      catch (Exception e)
      {
        Console.WriteLine($"Something failed {e}");
        return BadRequest();
      }
      if (stripeEvent.Type == "customer.subscription.deleted")
      {
        var session = stripeEvent.Data.Object as Stripe.Subscription;
        await _authService.CancelledSubscriptionUserUpdate(session.CustomerId);
        Console.WriteLine($"Session ID: {session}");
        // Take some action based on session.
      }
      if (stripeEvent.Type == "customer.subscription.created")
      {
        var session = stripeEvent.Data.Object as Stripe.Subscription;
        await _authService.SuccesfullSubscriptionUserUpdate(session.CustomerId);
        Console.WriteLine($"Session ID: {session}");
        // Take some action based on session.
      }

      return Ok();
    }

  }
}
