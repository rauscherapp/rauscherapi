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
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly ICommoditiesRepository _commoditiesRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarCommoditiesRateCommandHandler(ICommoditiesRateRepository commoditiesRateRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications,
                                                 ISymbolsRepository symbolsRepository,
                                                 ICommoditiesRepository commoditiesRepository) : base(uow, bus, notifications)
    {
      _commoditiesRateRepository = commoditiesRateRepository;
      Bus = bus;
      _symbolsRepository = symbolsRepository;
      _commoditiesRepository = commoditiesRepository;
    }
    public async Task<bool> Handle(CadastrarCommoditiesRateCommand message, CancellationToken cancellationToken)
    {
      var baseSymbols = await _symbolsRepository.ObterSymbolsAppVisible();

      List<CommoditiesRate> resultMapped = new List<CommoditiesRate>();

      if (baseSymbols.Count > 5)
      {
        var batchSize = 5;

        // Grouping the symbols into batches of 5
        var groupedSymbols = baseSymbols.Select((x, index) => new { Index = index, Value = x })
                                        .GroupBy(x => x.Index / batchSize)
                                        .Select(x => x.Select(v => v.Value.Code).ToList())
                                        .ToList();


        foreach (var group in groupedSymbols)
        {
          // Process each group of 5 symbols here
          // 'group' is a List<string> containing up to 5 symbols
          Console.WriteLine("Batch of symbols:");
          foreach (var symbol in group)
          {
            resultMapped.AddRange(await _commoditiesRepository.GetLatestCommodityRatesAsync("USD", group));
          }
        }
      }
      else
      {
        resultMapped.AddRange(await  _commoditiesRepository.GetLatestCommodityRatesAsync("USD", baseSymbols.Select(c => c.Code)));
      }

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

        var lastRate = await _commoditiesRateRepository.GetLastPriceBeforeTimestamp(message.Code, message.Timestamp);

        if (lastRate is not null)
        {
          commoditiesRate.CalculateVariation(lastRate.Price);
        }
        else
        {
          commoditiesRate.SetZerosVariation();
        }
        _commoditiesRateRepository.Add(commoditiesRate);

        if (Commit())
        {
          await Bus.RaiseEvent(new CadastrarCommoditiesRateEvent(
          result.Id,
          result.Timestamp,
          result.BaseCurrency,
          result.Date,
          result.SymbolCode,
          result.Unit,
          result.Price,
          result.Variationprice,
          result.Variationpricepercent,
          result.Isup
          ));
        }
      }
      return true;
    }

  }
}
