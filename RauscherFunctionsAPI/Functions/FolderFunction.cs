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

public class FolderFunction : BaseFunctions
{
  private readonly IFolderAppService _folderAppService;
  private readonly IMapper _mapper;
  private readonly IMediatorHandler _bus;

  public FolderFunction(
      IFolderAppService folderAppService,
      IMediatorHandler bus,
      IMapper mapper,
      INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
  {
    _folderAppService = folderAppService;
    _mapper = mapper;
    _bus = bus;
  }

  [FunctionName("GetFolders")]
  public async Task<IActionResult> GetFolders(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Folder")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing GET request for Folders.");

    var parameters = new FolderParameters();
    // Optionally: parse query parameters from request to populate 'parameters'

    try
    {
      var folders = await _folderAppService.ListarFolder(parameters);
      var result = _mapper.Map<IEnumerable<FolderViewModel>>(folders.Data).ShapeData(parameters.Fields);

      req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(folders.PaginationMetadata));
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error listing Folders: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("GetFolderById")]
  public async Task<IActionResult> GetFolderById(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Folder/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing GET request for Folder with ID: {id}");

    try
    {
      var result = await _folderAppService.ObterFolder(id);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error getting Folder by ID: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("CreateFolder")]
  public async Task<IActionResult> CreateFolder(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Folder")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing POST request to create a new Folder.");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var folderViewModel = JsonSerializer.Deserialize<FolderViewModel>(requestBody);

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
      var result = await _folderAppService.CadastrarFolder(folderViewModel);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error creating Folder: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("UpdateFolder")]
  public async Task<IActionResult> UpdateFolder(
      [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/Folder/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing PUT request to update Folder with ID: {id}");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var folderViewModel = JsonSerializer.Deserialize<FolderViewModel>(requestBody);

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
      var result = await _folderAppService.AtualizarFolder(folderViewModel);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error updating Folder: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }

  [FunctionName("DeleteFolder")]
  public async Task<IActionResult> DeleteFolder(
      [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/Folder/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing DELETE request to delete Folder with ID: {id}");

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
      var result = await _folderAppService.ExcluirFolder(id);
      return CreateResponse(result);
    }
    catch (Exception ex)
    {
      log.LogError($"Error deleting Folder: {ex.Message}");
      return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
  }
}
