using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RauscherFunctionsAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class AppParameterFunction : BaseFunctions
{
  private readonly IAppParametersAppService _appParameterAppService;
  private readonly IMapper _mapper;
  private readonly IMediatorHandler _bus;

  public AppParameterFunction(
      IAppParametersAppService appParameterAppService,
      IMediatorHandler bus,
      IMapper mapper,
      INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
  {
    _appParameterAppService = appParameterAppService;
    _mapper = mapper;
    _bus = bus;
  }

  [FunctionName("GetAppParameters")]
  public async Task<IActionResult> GetAppParameters(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/AppParameter")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing GET request for AppParameters.");

    var parameters = new AppParametersParameters();
    // Optionally: parse query parameters from request to populate 'parameters'

    try
    {
      var appParameters = await _appParameterAppService.ListarAppParameters(parameters);
      var result = _mapper.Map<IEnumerable<AppParametersViewModel>>(appParameters.Data).ShapeData(parameters.Fields);

      req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(appParameters.PaginationMetadata));
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error listing AppParameters: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("GetAppParameterById")]
  public async Task<IActionResult> GetAppParameterById(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/AppParameter/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing GET request for AppParameter with ID: {id}");

    try
    {
      var result = await _appParameterAppService.ObterAppParameters();
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error getting AppParameter: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("CreateAppParameter")]
  public async Task<IActionResult> CreateAppParameter(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/AppParameter")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing POST request to create a new AppParameter.");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var appParameterViewModel = JsonSerializer.Deserialize<AppParametersViewModel>(requestBody);

    if (!IsValidOperation())
    {
      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }

    try
    {
      var result = await _appParameterAppService.CadastrarAppParameters(appParameterViewModel);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error creating AppParameter: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("UpdateAppParameter")]
  public async Task<IActionResult> UpdateAppParameter(
      [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/UpdateAppParameter/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing PUT request to update AppParameter with ID: {id}");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var appParameterViewModel = JsonSerializer.Deserialize<AppParametersViewModel>(requestBody);

    if (!IsValidOperation())
    {
      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }

    try
    {
      var result = await _appParameterAppService.AtualizarAppParameters(appParameterViewModel);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error updating AppParameter: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("DeleteAppParameter")]
  public async Task<IActionResult> DeleteAppParameter(
      [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/AppParameter/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing DELETE request to delete AppParameter with ID: {id}");

    if (!IsValidOperation())
    {
      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }

    try
    {
      var result = await _appParameterAppService.ExcluirAppParameters(id);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error deleting AppParameter: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }
}
