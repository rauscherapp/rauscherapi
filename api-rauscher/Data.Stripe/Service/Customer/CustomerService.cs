//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using StripeApi.Models;
//using StripeApi.Options;

//namespace StripeApi.Service
//{
//  public class StripeCustomerService
//  {
//    private readonly StripeService _stripeService;
//    private readonly ILogger<StripeCustomerService> _logger;

//    public StripeCustomerService(StripeService stripeService, ILogger<StripeCustomerService> logger)
//    {
//      _stripeService = stripeService;
//      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//    }

//    public async Task<Customer> CreateCustomerAsync(string name, string email, string description)
//    {
//      _logger.LogInformation($"Handling: {nameof(StripeCustomerService)}");
//      var customerCreateOptions = new
//      {
//        name,
//        email,
//        description
//      };

//      return await _stripeService.PostAsync<Customer>("customers", customerCreateOptions);
//    }

//    public async Task<Customer> GetCustomerAsync(string customerId)
//    {
//      return await _stripeService.GetAsync<Customer>($"customers/{customerId}");
//    }
//  }
//}