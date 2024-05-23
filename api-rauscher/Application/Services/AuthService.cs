using APIs.Security.JWT;
using Application.Interfaces;
using System.Threading.Tasks;

namespace Application.Services
{
  public class AuthService : IAuthService
  {
    private readonly AccessManager _accessManager;

    public AuthService(AccessManager accessManager)
    {
      _accessManager = accessManager;
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
  }
}
