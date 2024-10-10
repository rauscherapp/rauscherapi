using Domain.Adapters.Vendors;
using Domain.Enum;
using Domain.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Commodities
{
  public class CommodititesRatesAdapter : IVendorRateAdapters
  {
    private readonly ILogger<CommodititesRatesAdapter> _logger;
    private readonly IEnumerable<ITradeReadRepository> _readRepositories;
    private readonly IAppParametersRepository _appParametersRepository;
    private readonly ISymbolsRepository _symbolsRepository;

    public CommodititesRatesAdapter(IEnumerable<ITradeReadRepository> readRepositories,
            ILogger<CommodititesRatesAdapter> logger,
            IAppParametersRepository appParametersRepository,
            ISymbolsRepository symbolsRepository)
    {
      _readRepositories = readRepositories;
      _logger = logger;
      _appParametersRepository = appParametersRepository;
      _symbolsRepository = symbolsRepository;
    }

    public async Task<IEnumerable<CommodityOpenHighLowClose>> GetOpeningRateAsync(string date, List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - Commoditites.Adapters", nameof(GetOpeningRateAsync));

      List<CommodityOpenHighLowClose> resultMapped = new List<CommodityOpenHighLowClose>();
      var parameters = _appParametersRepository.ObterAppParameters();
      if (parameters.CommoditiesApiOn)
      {
        symbols = symbols.Where(cr => cr.Vendor.Equals(VendorEnum.Commodity.Name)).ToList();       

        foreach (var symbol in symbols)
        {
          var repository = _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Commodity.Name));
          if (repository != null)
          {
            var apiOhlc = await repository.GetOpeningRateAsync(symbol.Code);
            if (apiOhlc != null)
            {
              resultMapped.Add(apiOhlc.FirstOrDefault());
            }
          }
        }
      }

      return resultMapped.Any() ? resultMapped : Enumerable.Empty<CommodityOpenHighLowClose>();
    }


    public async Task<IEnumerable<CommoditiesRate>> GetRateAsync(List<Symbols> symbols)
    {
      _logger.LogInformation("{method} - Commoditites.Adapters", nameof(GetRateAsync));
      symbols = symbols.Where(cr => cr.Vendor.Equals(VendorEnum.Commodity.Name)).ToList();

      var parameters = _appParametersRepository.ObterAppParameters();
      if (parameters.CommoditiesApiOn)
      {
        if (symbols.Count > 5)
        {
          var batchSize = 5;
          var groupedSymbols = symbols.Select((x, index) => new { Index = index, Value = x })
                                          .GroupBy(x => x.Index / batchSize)
                                          .Select(x => x.Select(v => v.Value.Code).ToList())
                                          .ToList();
          foreach (var group in groupedSymbols)
          {
            Console.WriteLine("Batch of symbols:");

            var codes = string.Join(",", group.Select(cr => cr));

            var vendorsTasks = _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Commodity.Name)).GetExchangeRateAsync(codes);

            await Task.WhenAll(vendorsTasks);

            return vendorsTasks.Result;
          }
        }
        else
        {

          var codes = string.Join(",", symbols.Select(cr => cr.Code));

          var vendorsTasks = _readRepositories.FirstOrDefault(x => x.GetRepositoryName().Equals(VendorEnum.Commodity.Name)).GetExchangeRateAsync(codes);

          await Task.WhenAll(vendorsTasks);

          return vendorsTasks.Result;
        }
      }
      return Enumerable.Empty<CommoditiesRate>();
    }

    public string GetVendorAdapter() => VendorEnum.Bacen.Name;
  }
}