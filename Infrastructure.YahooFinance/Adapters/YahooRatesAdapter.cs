using Domain.Adapters.Vendors;
using Domain.Enum;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.YahooFinance
{
  public class YahooRatesAdapter : IVendorRateAdapters
  {
    private readonly ILogger<YahooRatesAdapter> _logger;
    private readonly IEnumerable<ITradeReadRepository> _readRepositories;
    private readonly IAppParametersRepository _appParametersRepository;
    private readonly ISymbolsRepository _symbolsRepository;

    public YahooRatesAdapter(IEnumerable<ITradeReadRepository> readRepositories,
            ILogger<YahooRatesAdapter> logger,
            ISymbolsRepository symbolsRepository,
            IAppParametersRepository appParametersRepository)
    {
      _readRepositories = readRepositories;
      _logger = logger;
      _symbolsRepository = symbolsRepository;
      _appParametersRepository = appParametersRepository;
    }

    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - YahooFinance.Adapters", nameof(GetOpeningRateAsync));
      var parameters = _appParametersRepository.ObterAppParameters();
      if (parameters.YahooFinanceApiOn)
      {
        var codes = string.Join(",", symbols.Where(cr => cr.Vendor.Equals(VendorEnum.Yahoo.Name)).Select(cr => cr.Code));
        var results = _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Yahoo.Name)).GetOpeningRateAsync(codes);

        await Task.WhenAll(results);

        return results.Result;
      }
      return Enumerable.Empty<CommodityOpenHighLowClose>();
    }

    public async Task<IEnumerable<CommoditiesRate>> GetRateAsync(List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - YahooFinance.Adapters", nameof(GetRateAsync));
      var parameters = _appParametersRepository.ObterAppParameters();
      if (parameters.YahooFinanceApiOn)
      {
        var codes = string.Join(",", symbols.Where(cr => cr.Vendor.Equals(VendorEnum.Yahoo.Name)).Select(cr => cr.Code));

        var vendorsTasks = _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Yahoo.Name)).GetExchangeRateAsync(codes);

        await Task.WhenAll(vendorsTasks);

        return vendorsTasks.Result;
      }
      return Enumerable.Empty<CommoditiesRate>();
    }

    public string GetVendorAdapter() => VendorEnum.Bacen.Name;
  }
}