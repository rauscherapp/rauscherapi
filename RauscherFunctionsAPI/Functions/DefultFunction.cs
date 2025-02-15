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
  public static class DefultFunction
  {
    [FunctionName("MyHttpTrigger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v1/MyEndpoint")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Azure Function HTTP Trigger acionada.");

      // Lendo o corpo da requisição (caso seja um POST)
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      dynamic data = JsonConvert.DeserializeObject(requestBody);

      string name = data?.name ?? req.Query["name"];

      return name != null
          ? new OkObjectResult($"Hello, {name}!")
          : new BadRequestObjectResult("Por favor, passe um nome na query string ou no corpo da requisição.");
    }
  }
}
