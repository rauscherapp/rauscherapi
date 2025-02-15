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
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{
  public class RatesFunction : BaseFunctions
  {
    private readonly ISymbolsAppService _symbolsAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public RatesFunction(
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        ISymbolsAppService symbolsAppService) : base(notifications, bus)
    {
      _mapper = mapper;
      _bus = bus;
      _symbolsAppService = symbolsAppService;
    }

    [FunctionName("GetSymbolsRates")]
    [AllowAnonymous]
    public async Task<IActionResult> ListarCommoditiesRate(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/GetSymbolsRates")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request to list symbols with rates.");

      try
      {
        // Mapear os parâmetros da query para SymbolsParameters
        var parameters = MapQueryStringToSymbolsParameters(req.QueryString.Value, log);

        // Obter símbolos com taxas
        var posts = await _symbolsAppService.ListarSymbolsWithRate(parameters);

        // Adicionar headers de paginação
        req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(posts.PaginationMetadata));

        // Mapear para o ViewModel e aplicar ShapeData
        var result = _mapper.Map<IEnumerable<SymbolsViewModel>>(posts.Data).ShapeData(parameters.Fields);

        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error processing request: {ex.Message}");
        return new BadRequestObjectResult(new { Message = "Error processing request", Details = ex.Message });
      }
    }

    private SymbolsParameters MapQueryStringToSymbolsParameters(string queryString, ILogger log)
    {
      var parameters = new SymbolsParameters();

      if (string.IsNullOrEmpty(queryString))
        return parameters;

      var queryDictionary = QueryHelpers.ParseQuery(queryString);

      try
      {
        if (queryDictionary.TryGetValue("Id", out var id) && Guid.TryParse(id, out var parsedId))
          parameters.Id = parsedId;

        if (queryDictionary.TryGetValue("Code", out var code))
          parameters.Code = code.ToString();

        if (queryDictionary.TryGetValue("Name", out var name))
          parameters.Name = name.ToString();

        if (queryDictionary.TryGetValue("SymbolType", out var symbolType))
          parameters.SymbolType = symbolType.ToString();

        if (queryDictionary.TryGetValue("Appvisible", out var appvisible) && bool.TryParse(appvisible, out var parsedAppvisible))
          parameters.Appvisible = parsedAppvisible;

        log.LogInformation("Query parameters mapped successfully.");
      }
      catch (Exception ex)
      {
        log.LogError($"Error mapping query parameters: {ex.Message}");
      }

      return parameters;
    }
  }
}
