using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.QueryParameters;
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
  public class SymbolsController : ApiController
  {

    private readonly ISymbolsAppService _symbolsAppService;
    private readonly ICommoditiesRateAppService _commoditiesRateAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public SymbolsController(
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        IApiCredentialsAppService apiCredentialsAppService,
        ISymbolsAppService symbolsAppService,
        ICommoditiesRateAppService commoditiesRateAppService) : base(notifications, bus)
    {
      _mapper = mapper;
      _bus = bus;
      _symbolsAppService = symbolsAppService;
      _commoditiesRateAppService = commoditiesRateAppService;
    }
    [HttpPost("SymbolsApi")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Secret([FromQuery] SymbolsViewModel parameters)
    {
      //var result = await _symbolsAppService.AtualizarSymbolsApi(parameters);
      var result = await _commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());
      return CreateResponse(result);
    }
  }
}
