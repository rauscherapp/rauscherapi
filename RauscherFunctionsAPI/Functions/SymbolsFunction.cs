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
using RauscherFunctionsAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public class SymbolsFunction : BaseFunctions
{
  private readonly ISymbolsAppService _symbolsAppService;
  private readonly ICommoditiesRateAppService _commoditiesRateAppService;
  private readonly IMapper _mapper;
  private readonly IMediatorHandler _bus;

  public SymbolsFunction(
      IMediatorHandler bus,
      IMapper mapper,
      INotificationHandler<DomainNotification> notifications,
      ISymbolsAppService symbolsAppService,
      ICommoditiesRateAppService commoditiesRateAppService) : base(notifications, bus)
  {
    _mapper = mapper;
    _bus = bus;
    _symbolsAppService = symbolsAppService;
    _commoditiesRateAppService = commoditiesRateAppService;
  }

  [FunctionName("PostSymbolsApi")]
  public async Task<IActionResult> PostSymbolsApi(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/SymbolsApi")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing request to update Symbols API.");

    var queryParameters = req.GetQueryParameterDictionary();
    var parameters = JsonSerializer.Deserialize<SymbolsViewModel>(JsonSerializer.Serialize(queryParameters));

    var result1 = await _symbolsAppService.AtualizarSymbolsApi(parameters);
    var result = await _commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());

    return CreateResponse(result);
  }

  [FunctionName("CreateSymbol")]
  public async Task<IActionResult> CreateSymbol(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/CreateSymbol")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing request to create a new Symbol.");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var symbolViewModel = JsonSerializer.Deserialize<SymbolsViewModel>(requestBody);

    var result = await _symbolsAppService.CadastrarSymbols(symbolViewModel);

    return CreateResponse(result);
  }

  [FunctionName("DeleteSymbol")]
  [AllowAnonymous]
  public async Task<IActionResult> DeletePost(
      [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/DeleteSymbols/{id}")] HttpRequest req,
      Guid id,
      ILogger log)
  {
    log.LogInformation($"Processing DELETE request to delete Symbol with ID: {id}");

    var result = await _symbolsAppService.ExcluirSymbols(id);
    return CreateResponse(result);
  }


  [FunctionName("UpdateSymbol")]
  public async Task<IActionResult> UpdateSymbol(
      [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/UpdateSymbol/{id}")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing request to update a Symbol.");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var symbolViewModel = JsonSerializer.Deserialize<SymbolsViewModel>(requestBody);

    if (!IsValidOperation())
    {
      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }

    var result = await _symbolsAppService.AtualizarSymbols(symbolViewModel);

    return CreateResponse(result);
  }

  [FunctionName("UpdateSymbolFromApi")]
  public async Task<IActionResult> UpdateSymbolFromApi(
      [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/UpdateSymbolFromApi")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing request to update Symbol from API.");

    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    var symbolViewModel = JsonSerializer.Deserialize<SymbolsViewModel>(requestBody);

    if (!IsValidOperation())
    {
      return new BadRequestObjectResult(new
      {
        success = false,
        errors = GetNotificationMessages()
      });
    }

    var result = await _symbolsAppService.AtualizarSymbolsApi(symbolViewModel);

    return CreateResponse(result);
  }

  [FunctionName("SymbolsApi")]
  public async Task<IActionResult> GetSymbolsApi(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/SymbolsApi")] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("Processing GET request for Symbols.");

    var queryParameters = req.GetQueryParameterDictionary();
    var bindParameters = new BindParameters();
    var parameters = bindParameters.BindQueryParameters<SymbolsParameters>(queryParameters);

    var symbols = await _symbolsAppService.ListarSymbols(parameters);
    req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(symbols.PaginationMetadata));

    var result = _mapper.Map<IEnumerable<SymbolsViewModel>>(symbols.Data).ShapeData(parameters.Fields);
    return CreateResponseList(symbols.PaginationMetadata, result);
  }
}
