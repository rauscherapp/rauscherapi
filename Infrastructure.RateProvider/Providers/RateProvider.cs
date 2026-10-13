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

      // ToList materializa as tarefas: sem ele o Select seria reexecutado e cada fornecedor chamado duas vezes
      var vendorsTasks = _vendorRatesAdapter.Select(vendorAssets => vendorAssets.GetRateAsync(symbols)).ToList();
      var vendorsResults = await Task.WhenAll(vendorsTasks);

      return vendorsResults.Where(x => x != null).SelectMany(x => x).ToList();
    }

    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - Providers", nameof(GetOpeningRateAsync));

      // ToList materializa as tarefas: sem ele o Select seria reexecutado e cada fornecedor chamado duas vezes
      var vendorsTasks = _vendorRatesAdapter.Select(vendorAssets => vendorAssets.GetOpeningRateAsync(date, symbols)).ToList();
      var vendorsResults = await Task.WhenAll(vendorsTasks);

      return vendorsResults.Where(x => x != null).SelectMany(x => x).ToList();
    }
  }
}
