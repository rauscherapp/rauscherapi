using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Domain.QueryParameters;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RauscherFunctionsAPI
{
  public class WorkerFunction
  {
    private readonly IServiceProvider _serviceProvider;
    private static DateTime _lastOHLCUpdateDate = DateTime.MinValue;

    public WorkerFunction(IServiceProvider serviceProvider)
    {
      _serviceProvider = serviceProvider;
    }

    [FunctionName("WorkerFunction")]
    public async Task Run(
        [TimerTrigger("*/6000 * * * * *")] TimerInfo timer,
        ILogger log)
    {
      log.LogInformation($"WorkerFunction executing at: {DateTime.UtcNow}");

      using (var scope = _serviceProvider.CreateScope())
      {
        var appParametersAppService = scope.ServiceProvider.GetRequiredService<IAppParametersAppService>();
        var appParametersResult = await appParametersAppService.ObterAppParameters();
        log.LogInformation(System.Text.Json.JsonSerializer.Serialize(appParametersResult));
        var commoditiesRateAppService = scope.ServiceProvider.GetRequiredService<ICommoditiesRateAppService>();
        var symbolsAppService = scope.ServiceProvider.GetRequiredService<ISymbolsAppService>();

        TimeSpan marketOpeningHour = TimeSpan.Parse(appParametersResult.MarketOpeningHour);
        TimeSpan marketClosingHour = TimeSpan.Parse(appParametersResult.MarketClosingHour);
        int minutesIntervalJob = appParametersResult.MinutesIntervalJob;

        var currentTime = DateTime.Now.TimeOfDay;
        var currentDate = DateTime.Now.Date;

#if DEBUG
        //marketOpeningHour = TimeSpan.Parse("09:00");
        //marketClosingHour = TimeSpan.Parse("22:00");
#endif
        log.LogInformation("Market Opened. WorkerFunction will execute and update Symbols Rates.");
        if (currentTime >= marketOpeningHour && currentTime <= marketClosingHour)
        {
          if (_lastOHLCUpdateDate != currentDate)
          {
            await commoditiesRateAppService.AtualizarOHLCCommoditiesRate();
            _lastOHLCUpdateDate = currentDate;
          }

          await commoditiesRateAppService.RemoverCommoditiesRateAntigos();
          await commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());

          var commoditiesData = await symbolsAppService.ListarSymbolsWithRateForWorker(new SymbolsParameters { SymbolType = "commodity", OrderBy = "Appvisible desc" });

          var exchangesData = await symbolsAppService.ListarSymbolsWithRateForWorker(new SymbolsParameters { SymbolType = "exchange", OrderBy = "Appvisible desc" });

          log.LogInformation($"Processed {commoditiesData.Count()} commodities and {exchangesData.Count()} exchanges.");

          // Simula um delay baseado no intervalo do job
          await Task.Delay(TimeSpan.FromMilliseconds(minutesIntervalJob));
        }
        else
        {
          log.LogInformation("Market closed. WorkerFunction will wait for the next interval.");
        }
      }
    }
  }
}
