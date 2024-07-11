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
          var commoditiesRateAppService = scope.ServiceProvider.GetRequiredService<ICommoditiesRateAppService>();

          // Chame os métodos do AppService aqui
          await commoditiesRateAppService.CadastrarCommoditiesRate(new CommoditiesRateViewModel());
        }
        await Task.Delay(10000, stoppingToken);
      }
    }
  }
}
