using APIs.Security.JWT;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AuthService : IAuthService
  {
    private readonly IAccessManager _accessManager;
    IOptionsSnapshot<ParametersOptions> _parameters;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IAccessManager accessManager,
        IOptionsSnapshot<ParametersOptions> parameters,
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        ILogger<AuthService> logger)
    {
      _accessManager = accessManager ?? throw new ArgumentNullException(nameof(accessManager));
      _parameters = parameters ?? throw new ArgumentNullException("parameters error");
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _mapper = mapper;
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
      return (true, tokenMapped);
    }

    public async Task<bool> DeleteAccount(UserRequest model)
    {
      // Exige e-mail e senha corretos: antes bastava o e-mail para apagar qualquer conta
      var (_, isValid) = await _accessManager.ValidateCredentials(model);
      if (!isValid)
        return false;

      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user == null)
        return false;

      var result = await _userManager.DeleteAsync(user);
      return result.Succeeded;
    }

  }
}
