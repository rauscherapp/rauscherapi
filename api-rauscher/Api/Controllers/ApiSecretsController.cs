using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers
{

  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [Produces("application/json", "application/xml")]
  [Consumes("application/json", "application/xml")]
  [AllowAnonymous]
  public class ApiSecretsController : ApiController
  {
    private readonly IApiCredentialsAppService _apiCredentialsAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public ApiSecretsController(
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        IApiCredentialsAppService apiCredentialsAppService) : base(notifications, bus)
    {
      _mapper = mapper;
      _bus = bus;
      _apiCredentialsAppService = apiCredentialsAppService;
    }

    [HttpPost("Secrets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Secret([FromQuery] FolderParameters parameters)
    {      
      var result = await _apiCredentialsAppService.GerarApiCredentials(parameters.SLUG);
      return ResponseAction(result);
    }
  }
}
