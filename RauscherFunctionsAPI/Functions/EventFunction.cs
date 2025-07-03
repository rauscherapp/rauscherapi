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
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{
  public class EventRegistryFunction : BaseFunctions
  {
    private readonly IEventRegistryAppService _eventAppService;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _bus;

    public EventRegistryFunction(
        IEventRegistryAppService eventAppService,
        IMapper mapper,
        IMediatorHandler bus,
        INotificationHandler<DomainNotification> notifications)
        : base(notifications, bus)
    {
      _eventAppService = eventAppService;
      _mapper = mapper;
      _bus = bus;
    }

    [FunctionName("GetEventRegistry")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEventRegistry(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/EventRegistry")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing GET request for Event Registry.");

      // Parse query parameters as needed
      var parameters = new EventRegistryParameters();

      try
      {
        var events = await _eventAppService.ListarEventRegistry(parameters);

        req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(events.PaginationMetadata));
        var result = _mapper.Map<IEnumerable<EventRegistryViewModel>>(events.Data).ShapeData(parameters.Fields);

        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error getting Event Registry list: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("GetEventRegistryApp")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEventRegistryApp(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/EventRegistryApp")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing GET request for Event Registry App.");

      var parameters = new EventRegistryParameters();
      try
      {
        var events = await _eventAppService.ListarEventRegistryApp(parameters);

        req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(events.PaginationMetadata));
        var result = _mapper.Map<IEnumerable<EventRegistryViewModel>>(events.Data).ShapeData(parameters.Fields);

        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error getting Event Registry App list: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("GetEventRegistryById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEventRegistryById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/EventRegistry/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing GET request for Event Registry with ID: {id}");

      try
      {
        var result = await _eventAppService.ObterEventRegistry(id);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error getting Event Registry by ID: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("CreateEventRegistry")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateEventRegistry(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/EventRegistry")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request to create Event Registry.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var eventViewModel = JsonSerializer.Deserialize<EventRegistryViewModel>(requestBody);

      try
      {
        var result = await _eventAppService.CadastrarEventRegistry(eventViewModel);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error creating Event Registry: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("UpdateEventRegistry")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateEventRegistry(
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/EventRegistry/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing PATCH request to update Event Registry with ID: {id}");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var eventRegistryViewModel = JsonSerializer.Deserialize<EventRegistryViewModel>(requestBody);

      try
      {
        var result = await _eventAppService.AtualizarEventRegistry(eventRegistryViewModel);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error updating Event Registry: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("DeleteEventRegistry")]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteEventRegistry(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/EventRegistry/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing DELETE request to delete Event Registry with ID: {id}");

      try
      {
        var result = await _eventAppService.ExcluirEventRegistry(id);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error deleting Event Registry: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
