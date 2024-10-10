using Application.Helpers;
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
    public async Task<IActionResult> Post([FromQuery] SymbolsViewModel parameters)
    {
      var result1 = await _symbolsAppService.AtualizarSymbolsApi(parameters);
      var result = await _commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());
      return CreateResponse(result);
    }

    [HttpPost("CreateSymbol")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateSymbol([FromQuery] SymbolsViewModel parameters)
    {
      var result = await _symbolsAppService.CadastrarSymbols(parameters);
      return CreateResponse(result);
    }

    [HttpPatch("UpdateSymbol/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateSymbol([FromBody] SymbolsViewModel SymbolViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _symbolsAppService.AtualizarSymbols(SymbolViewModel);
      return CreateResponse(result);
    }

    [HttpPatch("UpdateSymbolFromApi")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateSymbolFromApi([FromBody] SymbolsViewModel symbolViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _symbolsAppService.AtualizarSymbolsApi(symbolViewModel);
      return CreateResponse(result);
    }

    [HttpGet("SymbolsApi")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] SymbolsParameters parameters)
    {
      var symbols = await _symbolsAppService.ListarSymbols(parameters);
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(symbols.PaginationMetadata));
      var result = _mapper.Map<IEnumerable<SymbolsViewModel>>(symbols.Data).ShapeData(parameters.Fields);
      return CreateResponseList(symbols.PaginationMetadata, result);
    }
  }
}
