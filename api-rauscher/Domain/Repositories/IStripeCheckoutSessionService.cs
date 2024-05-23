using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
  public interface IStripeCheckoutSessionService
  {
    Task<CheckoutSession> CreateCheckoutSessionAsync(string successUrl, string cancelUrl, List<LineItem> lineItems, string mode, string clientReferenceId = null, string customer = null, string customerEmail = null);
  }
}