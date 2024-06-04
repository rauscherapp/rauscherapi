using Stripe;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IStripeCustomerService
  {
    Task<Customer> CreateCustomerAsync(string email);
    Task<Customer> GetCustomerByEmailAsync(string customerId);
  }
}
