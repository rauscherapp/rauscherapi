using APIs.Security.JWT;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [Produces("application/json", "application/xml")]
  [Consumes("application/json", "application/xml")]
  [AllowAnonymous]
  public class AuthController : ApiController
  {
    private readonly AccessManager _accessManager;
    private readonly IMediatorHandler _bus;

    public AuthController(
      INotificationHandler<DomainNotification> notifications,
      IMediatorHandler bus,
      AccessManager accessManager)
      : base(notifications, bus)
    {
      _bus = bus;
      _accessManager = accessManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User model)
    {
      var user = new User { Email = model.Email };
      var result = _accessManager.CreateUser(model);

      if (result)
      {
        var resultValidation = _accessManager.ValidateCredentials(user);
        if(resultValidation.Item2)
          return Ok();

        ModelState.AddModelError(string.Empty, "Erro ao logar no sistema");

        return BadRequest(ModelState);
      }

      ModelState.AddModelError(string.Empty, "Erro criar usuario no sistema");

      return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User model)
    {
      var result = _accessManager.ValidateCredentials(model);

      if (result.Item2)
      {
        var token = _accessManager.GenerateToken(result.Item1);
        return Ok(token.AccessToken);
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(ModelState);
      }
    }
  }
}
