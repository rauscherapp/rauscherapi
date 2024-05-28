using APIs.Security.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    private readonly AccessManager _accessManager;
    public StripeListenerController()
    {
      _webhookSecret = "whsec_ac5085b7e96a855990b9c947aeaf114dd79c8c0c3d182565c4325722881b6028";
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

      if (stripeEvent.Type == "checkout.session.completed")
      {
        var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
        Console.WriteLine($"Session ID: {session.Id}");
        // Take some action based on session.
      }
      if (stripeEvent.Type == "payment_intent.succeeded")
      {
        var session = stripeEvent.Data.Object;
        Console.WriteLine($"Session ID: {session}");
        // Take some action based on session.
      }
      if (stripeEvent.Type == "customer.subscription.created")
      {
        var session = stripeEvent.Data.Object as Stripe.Subscription;
        Console.WriteLine($"Session ID: {session}");
        // Take some action based on session.
      }

      return Ok();
    }

  }
}
