using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{
  public class AboutUsFunction : BaseFunctions
  {
    private readonly IAboutUsAppService _aboutUsAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;
    public AboutUsFunction(IAboutUsAppService aboutUsAppService, IMediatorHandler bus, IMapper mapper, INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
    {
      _aboutUsAppService = aboutUsAppService;
      _bus = bus;
      _mapper = mapper;
    }

    [FunctionName("PatchAboutUs")]
    public async Task<IActionResult> Patch(
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/AboutUs")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing PATCH request for AboutUs.");

      // Read request body and bind to AboutUsViewModel
      var parameters = await System.Text.Json.JsonSerializer.DeserializeAsync<AboutUsViewModel>(req.Body);

      try
      {
        var result = await _aboutUsAppService.AtualizarAboutUs(parameters);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error updating AboutUs: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("GetAboutUs")]
    public async Task<IActionResult> Get(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/AboutUs")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing GET request for AboutUs.");

      try
      {
        var result = await _aboutUsAppService.ObterAboutUs();
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error getting AboutUs: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
  }
}
