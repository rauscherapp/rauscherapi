using Api.Controllers;
using APIs.Security.JWT;
using Application.Interfaces;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [Produces("application/json", "application/xml")]
  [Consumes("application/json", "application/xml")]
  [AllowAnonymous]
  public class AuthController : ApiController
  {
    private readonly IAuthService _authService;

    public AuthController(
        INotificationHandler<DomainNotification> notifications,
        IMediatorHandler bus,
        IAuthService authService)
        : base(notifications, bus)
    {
      _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRequest model)
    {
      var result = await _authService.Register(model);

      if (result)
      {
        return Ok();
      }

      ModelState.AddModelError(string.Empty, "Erro criar usuario no sistema");
      return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserRequest model)
    {
      var result = await _authService.Login(model);

      if (result.IsValid)
      {
        return Ok(result.Token);
      }
      else
      {
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(ModelState);
      }
    }
    [HttpPost("checkSubscription")]
    public async Task<IActionResult> CheckSubscription([FromBody] UserRequest model)
    {
       return Ok(await _authService.CheckSubscription(model));
    }
  }
}
