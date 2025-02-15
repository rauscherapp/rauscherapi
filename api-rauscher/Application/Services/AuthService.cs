using APIs.Security.JWT;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AuthService : IAuthService
  {
    private readonly IStripeSessionService _stripeSessionClient;
    private readonly IStripeCustomerService _stripeCustomerService;
    private readonly IAccessManager _accessManager; // Agora usa a interface
    IOptionsSnapshot<ParametersOptions> _parameters;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly string _priceId;

    public AuthService(
        IAccessManager accessManager, // Injeção correta
        IOptionsSnapshot<ParametersOptions> parameters,
        IStripeSessionService stripeSessionClient,
        UserManager<ApplicationUser> userManager,
        IStripeCustomerService stripeCustomerService,
        IMapper mapper)
    {
      _accessManager = accessManager ?? throw new ArgumentNullException(nameof(accessManager));
      _parameters = parameters ?? throw new ArgumentNullException("parameters error");
      _priceId = parameters?.Value?.StripePriceId ?? throw new ArgumentNullException(nameof(parameters));
      _stripeSessionClient = stripeSessionClient ?? throw new ArgumentNullException(nameof(stripeSessionClient));
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _stripeCustomerService = stripeCustomerService ?? throw new ArgumentNullException(nameof(stripeCustomerService));
      _mapper = mapper;
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
          throw new Exception($"Error Stripe {e.Message}");
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
          throw new Exception($"Error Stripe {e.Message}");
        }
      }
      return (true, tokenMapped);
    }

    public async Task<bool> SuccesfullSubscriptionUserUpdate(string customerId)
    {
      var customer = await _stripeCustomerService.GetCustomerByEmailAsync(customerId);
      var user = await _userManager.FindByEmailAsync(customer.Email);
      if (user == null) return false;

      user.HasValidStripeSubscription = true;
      await _userManager.UpdateAsync(user);
      return user.HasValidStripeSubscription;
    }

    public async Task<bool> CancelledSubscriptionUserUpdate(string customerId)
    {
      var customer = await _stripeCustomerService.GetCustomerByEmailAsync(customerId);
      var user = await _userManager.FindByEmailAsync(customer.Email);
      if (user == null) return false;

      user.HasValidStripeSubscription = false;
      await _userManager.UpdateAsync(user);
      return user.HasValidStripeSubscription;
    }
  }
}
