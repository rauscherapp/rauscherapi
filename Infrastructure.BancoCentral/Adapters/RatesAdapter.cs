using Domain.Adapters.Vendors;
using Domain.Enum;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BancoCentral
{
  public class RatesAdapter : IVendorRateAdapters
  {
    private readonly ILogger<RatesAdapter> _logger;
    private readonly IEnumerable<ITradeReadRepository> _readRepositories;

    public RatesAdapter(IEnumerable<ITradeReadRepository> readRepositories,
            ILogger<RatesAdapter> logger)
    {
      _readRepositories = readRepositories;
      _logger = logger;
    }

    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - BancoCentral.Adapters", nameof(GetOpeningRateAsync));

      return await _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Bacen.Name)).GetOpeningRateAsync(date);
    }

    public async Task<IEnumerable<CommoditiesRate>> GetRateAsync(List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - BancoCentral.Adapters", nameof(GetRateAsync));

      var date = DateTime.Now.ToString("MM-dd-yyyy");

      return await _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Bacen.Name)).GetExchangeRateAsync(date);

    }

    public string GetVendorAdapter() => VendorEnum.Bacen.Name;
  }
}