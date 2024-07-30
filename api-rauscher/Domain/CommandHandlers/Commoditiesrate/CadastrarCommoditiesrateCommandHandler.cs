using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class CadastrarCommoditiesRateCommandHandler : CommandHandler,
          IRequestHandler<CadastrarCommoditiesRateCommand, bool>
  {
    private readonly ICommoditiesRateRepository _commoditiesRateRepository;
    private readonly IBancoCentralRepository _bancoCentralRepository;
    private readonly IYahooFinanceRepository _yahooFinanceRepository;
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly ICommoditiesRepository _commoditiesRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarCommoditiesRateCommandHandler(ICommoditiesRateRepository commoditiesRateRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications,
                                                 ISymbolsRepository symbolsRepository,
                                                 ICommoditiesRepository commoditiesRepository,
                                                 IBancoCentralRepository bancoCentralRepository,
                                                 IYahooFinanceRepository yahooFinanceRepository) : base(uow, bus, notifications)
    {
      _commoditiesRateRepository = commoditiesRateRepository;
      Bus = bus;
      _symbolsRepository = symbolsRepository;
      _commoditiesRepository = commoditiesRepository;
      _bancoCentralRepository = bancoCentralRepository;
      _yahooFinanceRepository = yahooFinanceRepository;
    }
    public async Task<bool> Handle(CadastrarCommoditiesRateCommand message, CancellationToken cancellationToken)
    {

      List<CommoditiesRate> resultMapped = new List<CommoditiesRate>();
      #region GetPtaxRate Banco Central

      //var dates = new List<DateTime>();
      var date = DateTime.Now;

      //while (dates.Count < 10)
      //{
      //  if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
      //  {
      //    dates.Add(date);
      //  }
      //  date = date.AddDays(-1);
      //}

      //foreach (var iDate in dates.OrderBy(x => x.Date))
      //{
      //  var ptaxRate = await _bancoCentralRepository.GetExchangeRateAsync(iDate.ToString("MM-dd-yyyy"));
      //  resultMapped.AddRange(ptaxRate);
      //}

      var ptaxRate = await _bancoCentralRepository.GetExchangeRateAsync(date.ToString("MM-dd-yyyy"));
      resultMapped.AddRange(ptaxRate);


      #endregion
      #region Get Exchanges YahooFinance
      var exchangeRate = await _yahooFinanceRepository.GetExchangeRateAsync();
      resultMapped.AddRange(exchangeRate);

      #endregion

      #region CommoditiesAPI
      //var baseSymbols = await _symbolsRepository.ObterSymbolsAppVisible();      

      //if (baseSymbols.Count > 5)
      //{
      //  var batchSize = 5;
      //  var groupedSymbols = baseSymbols.Select((x, index) => new { Index = index, Value = x })
      //                                  .GroupBy(x => x.Index / batchSize)
      //                                  .Select(x => x.Select(v => v.Value.Code).ToList())
      //                                  .ToList();
      //  foreach (var group in groupedSymbols)
      //  {
      //    Console.WriteLine("Batch of symbols:");
      //    foreach (var symbol in group)
      //    {
      //      resultMapped.AddRange(await _commoditiesRepository.GetLatestCommodityRatesAsync("USD", group));
      //    }
      //  }
      //}
      //else
      //{
      //  resultMapped.AddRange(await _commoditiesRepository.GetLatestCommodityRatesAsync("USD", baseSymbols.Select(c => c.Code)));
      //}
      #endregion
      foreach (var result in resultMapped)
      {
        var commoditiesRate = new CommoditiesRate(
          result.Timestamp,
          result.BaseCurrency,
          result.Date,
          result.SymbolCode,
          result.Unit,
          result.Price,
          result.Variationprice,
          result.Variationpricepercent,
          result.Isup
          );

        var lastRate = await _commoditiesRateRepository.GetLastPriceBeforeTimestamp(result.SymbolCode, result.Timestamp);

        if (lastRate is not null)
        {
          commoditiesRate.CalculateVariation(lastRate.Price);
        }
        else
        {
          commoditiesRate.SetZerosVariation();
        }

        if (lastRate is null || (lastRate.Timestamp != commoditiesRate.Timestamp))
        {
          _commoditiesRateRepository.Add(commoditiesRate);

          Commit();
        }
      }
      return true;
    }

  }
}
