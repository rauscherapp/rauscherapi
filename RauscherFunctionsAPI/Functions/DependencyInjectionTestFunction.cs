using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Serilog;

namespace RauscherFunctionsAPI.Functions
{
    public class DependencyInjectionTestFunction
    {
        private readonly IAboutUsAppService _service;

        public DependencyInjectionTestFunction(IAboutUsAppService service)
        {
            _service = service;
        }

        [FunctionName("TestService")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "testservice")] HttpRequest req,
            ILogger log)
        {
            log.Information("TestService chamada");

            if (_service == null)
                return new BadRequestObjectResult("Serviço não foi injetado");

            return new OkObjectResult("Serviço injetado com sucesso");
        }
    }
}
