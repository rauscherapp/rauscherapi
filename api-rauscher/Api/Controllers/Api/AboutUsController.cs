using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [Produces("application/json", "application/xml")]
  [Consumes("application/json", "application/xml")]
  [AllowAnonymous]
  public class AboutUsController : ApiController
  {

    private readonly IAboutUsAppService _aboutUsAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public AboutUsController(
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        IAboutUsAppService aboutUsAppService
      ) : base(notifications, bus)
    {
      _mapper = mapper;
      _bus = bus;
      _aboutUsAppService = aboutUsAppService;

    }
    [HttpPost("AboutUs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromQuery] AboutUsViewModel parameters)
    {
      var result = await _aboutUsAppService.AtualizarAboutUs(parameters);
      return CreateResponse(result);
    }
    [HttpGet("AboutUs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] AboutUsViewModel parameters)
    {
      var result = await _aboutUsAppService.ObterAboutUs();
      return CreateResponse(result);
    }
  }
}
