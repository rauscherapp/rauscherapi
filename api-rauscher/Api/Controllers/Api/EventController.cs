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
  public class EventRegistryController : ApiController
  {
    private readonly IPropertyCheckerService _propertyCheckerService;
    private readonly IEventRegistryAppService _eventAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public EventRegistryController(
        IPropertyCheckerService propertyCheckerService,
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        IEventRegistryAppService eventAppService) : base(notifications, bus)
    {
      _propertyCheckerService = propertyCheckerService;
      _mapper = mapper;
      _bus = bus;
      _eventAppService = eventAppService;
    }
    [HttpGet("EventRegistry")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> EventRegistry([FromQuery] EventRegistryParameters parameters)
    {
      var events = await _eventAppService.ListarEventRegistry(parameters);

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(events.PaginationMetadata));
      var result = _mapper.Map<IEnumerable<EventRegistryViewModel>>(events.Data).ShapeData(parameters.Fields);
      return CreateResponse(result);
    }


    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("EventRegistry/{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> ObterFolder(Guid id)
    {
      var result = await _eventAppService.ObterEventRegistry(id);
      return CreateResponse(result);
    }

    [HttpPost("CreateEventRegistry")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateEventRegistry([FromBody] EventRegistryViewModel eventViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _eventAppService.CadastrarEventRegistry(eventViewModel);
      return CreateResponse(result);
    }
    [HttpPatch("UpdateEventRegistry/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateEventRegistry([FromBody] EventRegistryViewModel EventRegistryViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _eventAppService.AtualizarEventRegistry(EventRegistryViewModel);
      return CreateResponse(result);
    }
    [HttpDelete("DeleteEventRegistry/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteEventRegistry(Guid id)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _eventAppService.ExcluirEventRegistry(id);
      return CreateResponse(result);
    }
  }
}
