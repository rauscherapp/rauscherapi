using APIs.Security.JWT;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    private readonly IStripeSessionService _stripeSessionClient;
    private readonly IStripeCustomerService _stripeCustomerService;
    private readonly AccessManager _accessManager;
    private UserManager<ApplicationUser> _userManager;
    private readonly string _priceId;

    public AuthService(AccessManager accessManager, IOptionsSnapshot<ParametersOptions> parameters, IStripeSessionService stripeSessionClient, UserManager<ApplicationUser> userManager, IStripeCustomerService stripeCustomerService)
    {
      _accessManager = accessManager;
      _priceId = parameters.Value.StripePriceId;
      _stripeSessionClient = stripeSessionClient;
      _userManager = userManager;
      _stripeCustomerService = stripeCustomerService;
    }

    public async Task<(bool IsValid, APIs.Security.JWT.Token Token)> Register(UserRequest model)
    {
      var UserRequest = new UserRequest { Email = model.Email, Password = model.Password };
      var result = await _accessManager.CreateUser(model);

      if (result)
      {
        var resultValidation = await _accessManager.ValidateCredentials(UserRequest);
        if (resultValidation.Item2)
        {
          var token = _accessManager.GenerateToken(resultValidation.Item1);
          if (token.User is not null && !token.User.HasValidStripeSubscription)
          {
            try
            {
              var session = await _stripeSessionClient.CreateSessionAsync(model.Email);
              if (session is not null)
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
        return (resultValidation.isValid, null);
      }
      return (false, null);
    }

    public async Task<(bool IsValid, string Token)> Login(UserRequest model)
    {
      var result = await _accessManager.ValidateCredentials(model);

      if (result.Item2)
      {
        var token = _accessManager.GenerateToken(result.Item1);
        return (true, token.AccessToken);
      }
      return (false, null);
    }
    public async Task<bool> CheckSubscription(UserRequest model)
    {
      return (await _accessManager.CheckSubscription(model));
    }
    public async Task<(bool IsValid, APIs.Security.JWT.Token Token)> AppLogin(UserRequest model)
    {
      var result = await _accessManager.ValidateCredentials(model);

      if (result.Item2)
      {
        var token = _accessManager.GenerateToken(result.Item1);
        if (token.User is not null && !token.User.HasValidStripeSubscription)
        {
          try
          {
            var session = await _stripeSessionClient.CreateSessionAsync(model.Email);
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
    public async Task<bool> SuccesfullSubscriptionUserUpdate(string customerId)
    {
      var customer = await _stripeCustomerService.GetCustomerByEmailAsync(customerId);
      var user = await _userManager.FindByEmailAsync(customer.Email);
      user.HasValidStripeSubscription = true;
      await _userManager.UpdateAsync(user);
      return user?.HasValidStripeSubscription ?? false;
    }
  }
}
