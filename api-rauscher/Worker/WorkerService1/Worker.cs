using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace Worker
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private HubConnection _hubConnection;
    private DateTime _lastOHLCUpdateDate;


    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
      _logger = logger;
      _serviceProvider = serviceProvider;
      _lastOHLCUpdateDate = DateTime.MinValue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _hubConnection = new HubConnectionBuilder()
        .WithUrl("http://awsrestapiespuri.us-east-2.elasticbeanstalk.com/CommoditiesTradeHub").Build();

      await _hubConnection.StartAsync(stoppingToken);

      _logger.LogInformation("SignalR Hub connection started");
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        using (var scope = _serviceProvider.CreateScope())
        {
          var appParametersAppService = scope.ServiceProvider.GetRequiredService<IAppParametersAppService>();
          var appParametersResult = await appParametersAppService.ObterAppParameters();

          var commoditiesRateAppService = scope.ServiceProvider.GetRequiredService<ICommoditiesRateAppService>();
          var symbolsAppService = scope.ServiceProvider.GetRequiredService<ISymbolsAppService>();

          TimeSpan marketOpeningHour = TimeSpan.Parse(appParametersResult.MarketOpeningHour);
          TimeSpan marketClosingHour = TimeSpan.Parse(appParametersResult.MarketClosingHour);
          int minutesIntervalJob = appParametersResult.MinutesIntervalJob;

          var currentTime = DateTime.Now.TimeOfDay;
          var currentDate = DateTime.Now.Date;

#if DEBUG
          marketOpeningHour = TimeSpan.Parse("09:00");
          marketClosingHour = TimeSpan.Parse("22:00");
#endif

          if (currentTime >= marketOpeningHour && currentTime <= marketClosingHour)
          {
            if (_lastOHLCUpdateDate != currentDate)
            {
              var taskOHLC = commoditiesRateAppService.AtualizarOHLCCommoditiesRate();
              await Task.WhenAll(taskOHLC);

              _lastOHLCUpdateDate = currentDate;
            }

            var taskExclude = commoditiesRateAppService.RemoverCommoditiesRateAntigos();

            await Task.WhenAll(taskExclude);

            var taskInclude = commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());

            await Task.WhenAll(taskInclude);

            var data = await symbolsAppService.ListarSymbolsWithRateForWorker(new Domain.QueryParameters.SymbolsParameters() { SymbolType = "commodity", OrderBy = "Appvisible desc" });

            // Envia os dados para o Hub
            await _hubConnection.InvokeAsync("SendCommodities", data, stoppingToken);

            data = await symbolsAppService.ListarSymbolsWithRateForWorker(new Domain.QueryParameters.SymbolsParameters() { SymbolType = "exchange", OrderBy = "Appvisible desc" });
            // Envia os dados para o Hub
            await _hubConnection.InvokeAsync("SendExchanges", data, stoppingToken);

            // Espera pelo intervalo especificado nos parâmetros
            await Task.Delay(TimeSpan.FromMilliseconds(minutesIntervalJob), stoppingToken);
          }
          else
          {
            // Aguarde até o próximo minuto antes de verificar novamente
            await Task.Delay(TimeSpan.FromMilliseconds(60000), stoppingToken);
          }
        }
      }
    }
  }
}
