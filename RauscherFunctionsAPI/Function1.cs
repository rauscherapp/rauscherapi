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
  public static class Function1
  {
    [FunctionName("Ping")]
    public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ping")] HttpRequest req,
    ILogger log)
        {
            return new OkObjectResult(new { status = "ok", time = DateTime.UtcNow });
        }
    }
}
