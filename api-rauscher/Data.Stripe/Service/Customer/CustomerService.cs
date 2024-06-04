using Domain.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;
using Stripe;

namespace StripeApi.Service
{
  public class StripeCustomerService : IStripeCustomerService
  {
    private readonly IStripeClient _stripeClient;
    public StripeCustomerService(IOptionsSnapshot<ParametersOptions> parameters)
    {
      _stripeClient = new StripeClient(parameters.Value.StripeApiSecret);
    }
    public async Task<Customer> GetCustomerByEmailAsync(string customerId)
    {
      var service = new CustomerService(_stripeClient);
      var customer = await service.GetAsync(customerId);

      return customer;
    }
    public async Task<Customer> CreateCustomerAsync(string email)
    {
      var options = new CustomerCreateOptions
      {
        Email = email,
      };
      var service = new CustomerService(_stripeClient);
      Customer customer = await service.CreateAsync(options);

      return customer;
    }
  }
}
