using Domain.Adapters.Providers;
using Domain.Adapters.Vendors;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.RateProvider.Providers
{
  public class RateProvider : IRateProvider
  {
    private readonly IEnumerable<IVendorRateAdapters> _vendorRatesAdapter;
    private readonly ILogger<RateProvider> _logger;

    public RateProvider(IEnumerable<IVendorRateAdapters> vendorRatesAdapter, ILogger<RateProvider> logger)
    {
      _vendorRatesAdapter = vendorRatesAdapter;
      _logger = logger;
    }

    public async Task<IEnumerable<CommoditiesRate>> GetRateAsync(List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - Providers", nameof(GetRateAsync));

      // Executa todas as tarefas de forma assíncrona e aguarda a conclusão de todas
      var vendorsTasks = _vendorRatesAdapter.Select(vendorAssets => vendorAssets.GetRateAsync(symbols));
      await Task.WhenAll(vendorsTasks);  // Use await aqui

      var vendorsResults = vendorsTasks.Select(x => x.Result).Where(x => x != null);

      // Processa os resultados das tarefas após aguardar sua conclusão
      var vendorRates = vendorsResults.Where(x => x != null).SelectMany(x => x).ToList();
      return vendorRates;
    }

    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - Providers", nameof(GetOpeningRateAsync));

      // Executa todas as tarefas de forma assíncrona e aguarda a conclusão de todas
      var vendorsTasks = _vendorRatesAdapter.Select(vendorAssets => vendorAssets.GetOpeningRateAsync(date, symbols));
      await Task.WhenAll(vendorsTasks);  // Use await aqui

      var vendorsResults = vendorsTasks.Select(x => x.Result).Where(x => x != null);
      // Processa os resultados das tarefas após aguardar sua conclusão
      var openingRates = vendorsResults.Where(x => x != null).SelectMany(x => x);
      return openingRates;
    }
  }
}
