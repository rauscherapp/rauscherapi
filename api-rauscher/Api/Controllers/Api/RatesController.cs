using Application.Helpers;
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
  public class RatesController : ApiController
  {

    private readonly ISymbolsAppService _symbolsAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public RatesController(
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        ISymbolsAppService symbolsAppService) : base(notifications, bus)
    {
      _mapper = mapper;
      _bus = bus;
      _symbolsAppService = symbolsAppService;
    }
    [HttpPost("GetSymbolsRates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> ListarCommoditiesRate([FromQuery] SymbolsParameters parameters)
    {
      var posts = await _symbolsAppService.ListarSymbolsWithRate(parameters);

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(posts.PaginationMetadata));
      var result = _mapper.Map<IEnumerable<SymbolsViewModel>>(posts.Data).ShapeData(parameters.Fields);
      return CreateResponse(result);
    }
  }
}
