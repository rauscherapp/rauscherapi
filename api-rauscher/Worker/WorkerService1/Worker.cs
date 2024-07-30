using Application.Interfaces;
using Application.ViewModels;

namespace Worker
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
    {
      _logger = logger;
      _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        using (var scope = _serviceProvider.CreateScope())
        {
          var appParametersAppService = scope.ServiceProvider.GetRequiredService<IAppParametersAppService>();
          var appParametersResult = await appParametersAppService.ObterAppParameters();

          var commoditiesRateAppService = scope.ServiceProvider.GetRequiredService<ICommoditiesRateAppService>();

          // Parse the market opening and closing hours
          TimeSpan marketOpeningHour = TimeSpan.Parse(appParametersResult.MarketOpeningHour);
          TimeSpan marketClosingHour = TimeSpan.Parse(appParametersResult.MarketClosingHour);
          int minutesIntervalJob = appParametersResult.MinutesIntervalJob;

          var currentTime = DateTime.Now.TimeOfDay;

          if (currentTime >= marketOpeningHour && currentTime <= marketClosingHour)
          {
            // Chame os métodos do AppService aqui
            await commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());

            // Espera pelo intervalo especificado nos parâmetros
            await Task.Delay(TimeSpan.FromMinutes(minutesIntervalJob), stoppingToken);
          }
          else
          {
            // Aguarde até o próximo minuto antes de verificar novamente
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
          }
        }
      }
    }
  }
}
