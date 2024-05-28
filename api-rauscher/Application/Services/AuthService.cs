using APIs.Security.JWT;
using Application.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AuthService : IAuthService
  {
    private readonly IStripeClient _stripeClient;
    private readonly AccessManager _accessManager;
    private readonly string _priceId;

    public AuthService(AccessManager accessManager, IOptionsSnapshot<ParametersOptions> parameters)
    {
      _accessManager = accessManager;
      _priceId = parameters.Value.StripePriceId;
      _stripeClient = new StripeClient(parameters.Value.StripeApiSecret);
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
    public async Task<bool> CheckSubscription(UserRequest model)
    {
      return await _accessManager.CheckSubscription(model);
    }
    public async Task<(bool IsValid, APIs.Security.JWT.Token Token)> AppLogin(UserRequest model)
    {
      var result = _accessManager.ValidateCredentials(model);

      if (result.Item2)
      {
        var token = _accessManager.GenerateToken(result.Item1);
        if (token.User is not null && !token.User.HasValidStripeSubscription)
        {
          var options = new SessionCreateOptions
          {
            SuccessUrl = $"http://www.uol.com.br",
            CancelUrl = $"http://ge.globo.com",
            Mode = "subscription",
            LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = _priceId,
                        Quantity = 1,
                    },
                },
          };
          var service = new SessionService(_stripeClient);
          try
          {
            var session = await service.CreateAsync(options);
            if(session is not null)
            {
              token.User.StripeSubscriptionLink = session.Url;
              return (true, token);
            }
            return (true, token);
          }
          catch (StripeException e)
          {
            throw new Exception($"Error Stripe {e.Message}");
          }

          catch (Exception ex)
          {
            throw;
          }

        }
        return (true, token);
      }

      return (false, null);
    }
  }
}
