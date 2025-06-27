using Domain.Model;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
  public interface IStripeCheckoutSessionService
  {
    Task<Session> CreateCheckoutSessionAsync(string successUrl,string cancelUrl,List<SessionLineItemOptions> lineItems,string clientReferenceId = null,string customer = null,string customerEmail = null);  }
}