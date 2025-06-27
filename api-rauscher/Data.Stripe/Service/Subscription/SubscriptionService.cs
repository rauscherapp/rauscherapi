using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;
using Stripe;

namespace Data.Stripe.Api.Service
{
  public class StripeSubscriptionService : IStripeSubscriptionService
  {
    private readonly IStripeClient _stripeClient;
    public StripeSubscriptionService(IOptionsSnapshot<ParametersOptions> parameters)
    {
      _stripeClient = new StripeClient(parameters.Value.StripeApiSecret);
    }
    public async Task<bool> CancelCustomerSubscription(string subscriptionId)
    {
      var service = new SubscriptionService(_stripeClient);
      
      var subscription = await service.CancelAsync(subscriptionId);

      return (subscription is null);
    }
    public async Task<List<Subscription>> ListAllSubscriptionsFromCustomer(string customerId)
    {
      var service = new SubscriptionService(_stripeClient);
      var options = new SubscriptionListOptions
      {
        Customer = customerId,
        Limit = 10 // Define um limite para evitar sobrecarga na consulta
      };

      var subscriptions = await service.ListAsync(options);

      return subscriptions.ToList();
    }
  }
}
