using Domain.Model;
using Domain.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StripeApi.Options;

namespace StripeApi.Service
{
  public class StripeCheckoutSessionService : IStripeCheckoutSessionService
  {
    private readonly StripeService _stripeService;
    private readonly ILogger<StripeCheckoutSessionService> _logger;
    private readonly string _priceId;

    public StripeCheckoutSessionService(StripeService stripeService, ILogger<StripeCheckoutSessionService> logger, IOptions<StripeApiOptions> options)
    {
      _stripeService = stripeService ?? throw new ArgumentNullException(nameof(stripeService));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _priceId = options.Value.PriceId;
    }

    public async Task<CheckoutSession> CreateCheckoutSessionAsync(
        string successUrl,
        string cancelUrl,
        List<LineItem> lineItems,
        string mode,
        string clientReferenceId = null,
        string customer = null,
        string customerEmail = null)
    {
      var sessionCreateOptions = new
      {
        success_url = successUrl,
        cancel_url = cancelUrl,
        line_items = lineItems,
        mode = Enum.GetName(CheckoutSessionMode.Subscription),
        client_reference_id = clientReferenceId,
        customer = customer,
        customer_email = customerEmail,
        payment_method_types = new List<string> { "card" }
      };

      try
      {
        _logger.LogInformation("Creating Checkout Session");
        var session = await _stripeService.PostAsync<CheckoutSession>("checkout/sessions", sessionCreateOptions);
        _logger.LogInformation("Checkout Session created successfully");
        return session;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred while creating Checkout Session");
        throw;
      }
    }
  }
}
