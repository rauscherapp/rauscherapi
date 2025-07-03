using APIs.Security.JWT;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AuthService : IAuthService
  {
    private readonly IStripeSessionService _stripeSessionClient;
    private readonly IStripeCustomerService _stripeCustomerService;
    private readonly IStripeSubscriptionService _stripeSubscriptionService;
    private readonly IAccessManager _accessManager;
    IOptionsSnapshot<ParametersOptions> _parameters;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly string _priceId;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IAccessManager accessManager,
        IOptionsSnapshot<ParametersOptions> parameters,
        IStripeSessionService stripeSessionClient,
        UserManager<ApplicationUser> userManager,
        IStripeCustomerService stripeCustomerService,
        IMapper mapper,
        IStripeSubscriptionService stripeSubscriptionService,
        ILogger<AuthService> logger)
    {
      _accessManager = accessManager ?? throw new ArgumentNullException(nameof(accessManager));
      _parameters = parameters ?? throw new ArgumentNullException("parameters error");
      _priceId = parameters?.Value?.StripePriceId ?? throw new ArgumentNullException(nameof(parameters));
      _stripeSessionClient = stripeSessionClient ?? throw new ArgumentNullException(nameof(stripeSessionClient));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _stripeCustomerService = stripeCustomerService ?? throw new ArgumentNullException(nameof(stripeCustomerService));
      _mapper = mapper;
      _stripeSubscriptionService = stripeSubscriptionService;
      _logger = logger;
    }

    public async Task<(bool IsValid, TokenViewModel Token)> Register(UserRequest model)
    {
      var userRequest = new UserRequest { Email = model.Email, Password = model.Password };
      var result = await _accessManager.CreateUser(model);

      if (!result)
        return (false, null);

      var resultValidation = await _accessManager.ValidateCredentials(userRequest);
      if (!resultValidation.Item2)
        return (false, null);

      var token = _accessManager.GenerateToken(resultValidation.Item1);
      var tokenMapped = _mapper.Map<TokenViewModel>(token);

      tokenMapped.AppParameters = new AppParametersViewModel()
      {
        WebSiteUrl = _parameters.Value.WebSiteUrl,
        EmailSender = _parameters.Value.EmailSender,
        EmailReceiver = _parameters.Value.EmailReceiver,
        InstagramUrl = _parameters.Value.InstagramUrl,
        WhatsAppNumber = _parameters.Value.WhatsappNumber,
        ContactNumber = _parameters.Value.ContactNumber
      };
      if (token.User is not null && !token.User.HasValidStripeSubscription)
      {
        try
        {
          var session = await _stripeSessionClient.CreateSessionAsync(model.Email);
          if (session is not null)
          {
            token.User.StripeSubscriptionLink = session.Url;
          }
        }
        catch (StripeException e)
        {
          _logger.LogError(e, "Error Stripe {Message}", e.Message);
          throw;
        }

      }
      return (true, tokenMapped);
    }

    public async Task<(bool IsValid, string Token)> Login(UserRequest model)
    {
      var result = await _accessManager.ValidateCredentials(model);

      return result.Item2
          ? (true, _accessManager.GenerateToken(result.Item1).AccessToken)
          : (false, null);
    }

    public async Task<bool> CheckSubscription(UserRequest model) =>
        await _accessManager.CheckSubscription(model);

    public async Task<(bool IsValid, TokenViewModel Token)> AppLogin(UserRequest model)
    {
      var result = await _accessManager.ValidateCredentials(model);

      if (!result.Item2)
        return (false, new TokenViewModel() { Authenticated = false });

      var token = _accessManager.GenerateToken(result.Item1);
      var tokenMapped = _mapper.Map<TokenViewModel>(token);
      tokenMapped.AppParameters = new AppParametersViewModel()
      {
        WebSiteUrl = _parameters.Value.WebSiteUrl,
        EmailSender = _parameters.Value.EmailSender,
        EmailReceiver = _parameters.Value.EmailReceiver,
        InstagramUrl = _parameters.Value.InstagramUrl,
        WhatsAppNumber = _parameters.Value.WhatsappNumber,
        ContactNumber = _parameters.Value.ContactNumber
      };
      if (token.User is not null && !token.User.HasValidStripeSubscription)
      {
        try
        {
          var session = await _stripeSessionClient.CreateSessionAsync(model.Email);
          if (session is not null)
          {
            token.User.StripeSubscriptionLink = session.Url;
          }
        }
        catch (StripeException e)
        {
          _logger.LogError(e, "Error Stripe {Message}", e.Message);
          throw;
        }
      }
      return (true, tokenMapped);
    }

    public async Task<bool> SuccesfullSubscriptionUserUpdate(string customerId)
    {
      var customer = await _stripeCustomerService.GetCustomerByIdAsync(customerId);
      var user = await _userManager.FindByEmailAsync(customer.Email);
      if (user == null) return false;

      user.HasValidStripeSubscription = true;
      user.CustomerIdStripe = customerId;
      await _userManager.UpdateAsync(user);
      return user.HasValidStripeSubscription;
    }

    public async Task<bool> CancelledSubscriptionUserUpdate(string customerId)
    {
      var customer = await _stripeCustomerService.GetCustomerByIdAsync(customerId);
      var user = await _userManager.FindByEmailAsync(customer.Email);
      if (user == null) return true;

      user.HasValidStripeSubscription = false;
      await _userManager.UpdateAsync(user);
      return user.HasValidStripeSubscription;
    }
    public async Task<bool> DeleteAccount(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);

      if (user == null)
      {
        return false;
      }

      try
      {
        var subscriptions = await _stripeSubscriptionService.ListAllSubscriptionsFromCustomer(user.CustomerIdStripe);
        if (subscriptions is not null && subscriptions.Any())
        {
          foreach (var sub in subscriptions)
          {
            await _stripeSubscriptionService.CancelCustomerSubscription(sub.Id);
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception($"Erro ao cancelar assinatura do Stripe: {ex.Message}");
      }
      var result = await _userManager.DeleteAsync(user);
      return result.Succeeded;
    }

  }
}
