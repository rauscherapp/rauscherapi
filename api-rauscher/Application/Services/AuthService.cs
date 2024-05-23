using APIs.Security.JWT;
using Application.Interfaces;
using Domain.Model;
using Domain.Options;
using Domain.Repository;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AuthService : IAuthService
  {
    private readonly AccessManager _accessManager;
    private readonly IStripeCheckoutSessionService _stripeCheckoutSessionService;
    private readonly string _priceId;

    public AuthService(AccessManager accessManager, IStripeCheckoutSessionService stripeCheckoutSessionService, IOptions<ParametersOptions> parameters)
    {
      _accessManager = accessManager;
      _stripeCheckoutSessionService = stripeCheckoutSessionService;
      _priceId = parameters.Value.StripePriceId;
    }

    public async Task<bool> Register(UserRequest model)
    {
      var UserRequest = new UserRequest { Email = model.Email };
      var result = _accessManager.CreateUser(model);

      if (result)
      {
        var resultValidation = _accessManager.ValidateCredentials(UserRequest);
        return resultValidation.Item2;
      }

      return false;
    }

    public async Task<(bool IsValid, string Token)> Login(UserRequest model)
    {
      var result = _accessManager.ValidateCredentials(model);

      if (result.Item2)
      {
        var token = _accessManager.GenerateToken(result.Item1);
        return (true, token.AccessToken);
      }

      return (false, null);
    }
    public async Task<(bool IsValid, Token Token)> AppLogin(UserRequest model)
    {
      var result = _accessManager.ValidateCredentials(model);

      if (result.Item2)
      {
        var token = _accessManager.GenerateToken(result.Item1);
        if (token.User is not null && !token.User.HasValidStripeSubscription)
        {
          var options = new CheckoutSession
          {
            SuccessUrl = $"/success.html?session_id={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"/canceled.html",
            Mode = "subscription",
            LineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        Price = new Price {
                          Id = _priceId
                        },
                        Quantity = 1,
                    },
                },
            // AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
          };
        }
        return (true, token);
      }

      return (false, null);
    }
  }
}
