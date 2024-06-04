using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace StripeApi.Service
{
  public class StripeSessionService : IStripeSessionService
  {
    private readonly IStripeClient _stripeClient;
    private readonly IStripeCustomerService _stripeCustomerService;
    private readonly string _stripePriceId;
    private readonly int _stripeTrialPeriod;

    public StripeSessionService(IOptionsSnapshot<ParametersOptions> parameters, IStripeCustomerService stripeCustomerService)
    {
      _stripeClient = new StripeClient(parameters.Value.StripeApiSecret);
      _stripePriceId = parameters.Value.StripePriceId;
      _stripeTrialPeriod = parameters.Value.StripeTrialPeriod;
      _stripeCustomerService = stripeCustomerService;
    }

    public async Task<Session> CreateSessionAsync(string email)
    {
      try
      {
        var customer = await _stripeCustomerService.CreateCustomerAsync(email);

        var options = new SessionCreateOptions
        {         
          SuccessUrl = $"https://www.uol.com.br",
          CancelUrl = $"https://www.uol.com.br",
          Mode = "subscription",
          LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = _stripePriceId,
                        Quantity = 1                        
                    },        },
          Customer = customer.Id,
          SubscriptionData = new Stripe.Checkout.SessionSubscriptionDataOptions
          {
            TrialPeriodDays = _stripeTrialPeriod
          }
        };
        var service = new SessionService(_stripeClient);

        var session = await service.CreateAsync(options);

        if (session is not null)
        {          
          return session;
        }
        return null;
      }
      catch (StripeException e)
      {
        throw new Exception($"Error Stripe {e.Message}");
      }

      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
