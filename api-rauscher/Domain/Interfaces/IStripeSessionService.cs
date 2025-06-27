using Stripe.Checkout;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IStripeSessionService
  {
    Task<Session> CreateSessionAsync(string priceId);
  }
}
