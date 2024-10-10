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

      // Read query parameters from request
      var queryString = req.QueryString.HasValue ? req.QueryString.Value : "";
      var parameters = new SymbolsParameters(); // Parse query parameters as needed

      // Get symbols with rates from the service
      var posts = await _symbolsAppService.ListarSymbolsWithRate(parameters);

      // Add pagination headers to the response
      req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(posts.PaginationMetadata));

      // Map the data to the view model
      var result = _mapper.Map<IEnumerable<SymbolsViewModel>>(posts.Data).ShapeData(parameters.Fields);
      return CreateResponse(result);
    }
  }
}
