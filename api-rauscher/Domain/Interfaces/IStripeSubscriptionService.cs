using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IStripeSubscriptionService
  {
    Task<bool> CancelCustomerSubscription(string customerId);
    Task<List<Subscription>> ListAllSubscriptionsFromCustomer(string customerId);
  }
}
