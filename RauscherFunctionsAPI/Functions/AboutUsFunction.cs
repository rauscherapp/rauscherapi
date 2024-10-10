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
    [AllowAnonymous]
    public async Task<IActionResult> Patch(
        [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/AboutUs")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing PATCH request for AboutUs.");

      // Read request body and bind to AboutUsViewModel
      var parameters = await System.Text.Json.JsonSerializer.DeserializeAsync<AboutUsViewModel>(req.Body);

      var result = await _aboutUsAppService.AtualizarAboutUs(parameters);
      return CreateResponse(result);
    }

    [FunctionName("GetAboutUs")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/AboutUs")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing GET request for AboutUs.");

      var result = await _aboutUsAppService.ObterAboutUs();
      return CreateResponse(result);
    }
  }
}
