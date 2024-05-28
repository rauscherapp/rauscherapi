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
using System;
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
  public class AppParameterController : ApiController
  {
    private readonly IAppParametersAppService _appParameterAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public AppParameterController(
        IAppParametersAppService appParameterAppService,
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
    {
      _appParameterAppService = appParameterAppService;
      _mapper = mapper;
      _bus = bus;
    }

    [HttpGet("AppParameter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> AppParameter([FromQuery] AppParametersParameters parameters)
    {
      var AppParameters = await _appParameterAppService.ListarAppParameters(parameters);
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(AppParameters.PaginationMetadata));
      var result = _mapper.Map<IEnumerable<AppParametersViewModel>>(AppParameters.Data).ShapeData(parameters.Fields);
      return CreateResponse(result);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("AppParameter/{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> ObterAppParameter(Guid id)
    {
      var result = await _appParameterAppService.ObterAppParameters(id);
      return CreateResponse(result);
    }


    [HttpPost("CreateAppParameter")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAppParameter([FromBody] AppParametersViewModel AppParameterViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _appParameterAppService.CadastrarAppParameters(AppParameterViewModel);
      return CreateResponse(result);
    }
    [HttpPut("UpdateAppParameter/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateAppParameter([FromBody] AppParametersViewModel AppParameterViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _appParameterAppService.AtualizarAppParameters(AppParameterViewModel);
      return CreateResponse(result);
    }
    [HttpDelete("DeleteAppParameter/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteAppParameter(Guid id)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _appParameterAppService.ExcluirAppParameters(id);
      return CreateResponse(result);
    }
  }
}
