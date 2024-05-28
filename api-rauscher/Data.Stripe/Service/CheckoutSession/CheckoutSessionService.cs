//using Domain.Options;
//using Domain.Repository;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Stripe;
//using Stripe.Checkout;
//using StripeApi.Options;

//namespace StripeApi.Service
//{
//  public class StripeCheckoutSessionService : IStripeCheckoutSessionService
//  {
//    private readonly StripeService _stripeService;
//    private readonly IStripeClient _stripeClient;
//    private readonly ILogger<StripeCheckoutSessionService> _logger;
//    private readonly string _priceId;

//    public StripeCheckoutSessionService(StripeService stripeService, ILogger<StripeCheckoutSessionService> logger, IOptionsSnapshot<ParametersOptions> options)
//    {
//      _stripeService = stripeService ?? throw new ArgumentNullException(nameof(stripeService));
//      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//      _priceId = options.Value.StripePriceId;
//      StripeConfiguration.ApiKey = options.Value.StripeApiSecret;
//    }

//    public async Task<Session> CreateCheckoutSessionAsync(
//        string successUrl,
//        string cancelUrl,
//        List<SessionLineItemOptions> lineItems,
//        string clientReferenceId = null,
//        string customer = null,
//        string customerEmail = null)
//    {
//      var sessionCreateOptions = new SessionCreateOptions
//      {
//        SuccessUrl = successUrl,
//        CancelUrl = cancelUrl,
//        LineItems = lineItems,
//        Mode = "subscription",
//        PaymentMethodTypes = new List<string> { "card" }
//      };
//      //sk_test_51ObU9bDdT5dkn1Yg3W14TjIRePJFQNdgldSgrpz5ktSnGwi6nhv9R1VeIn3elGSYx0mLz9QzY027XfZWxiimqfrW00Hg04rSeM
//      try
//      {
//        _logger.LogInformation("Creating Checkout Session");
//        //var session = await _stripeService.PostAsync<CheckoutSession>("checkout/sessions", sessionCreateOptions);
//        var service = new SessionService();
//        try
//        {
//          var session = await service.CreateAsync(sessionCreateOptions);
//          return session;
//        }
//        catch (StripeException e)
//        {
//          _logger.LogError(e, "Error occurred while creating Checkout Session");
//          throw new Exception($"Error Stripe {e.Message}");
//        }
//      }
//      catch (Exception ex)
//      {
//        _logger.LogError(ex, "Error occurred while creating Checkout Session");
//        throw;
//      }
//    }
//  }
//}
