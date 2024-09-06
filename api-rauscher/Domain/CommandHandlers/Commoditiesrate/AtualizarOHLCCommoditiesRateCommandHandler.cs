using Domain.Adapters.Providers;
using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class AtualizarOHLCCommoditiesRateCommandHandler : CommandHandler,
          IRequestHandler<AtualizarOHLCCommoditiesRateCommand, bool>
  {
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly IRateProvider _rateProvider;
    private readonly ICommodityOpenHighLowCloseRepository _commodityOpenHighLowCloseRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarOHLCCommoditiesRateCommandHandler(ISymbolsRepository symbolsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications,
                                                 ICommodityOpenHighLowCloseRepository commodityOpenHighLowCloseRepository,
                                                 IRateProvider rateProvider) : base(uow, bus, notifications)
    {
      _symbolsRepository = symbolsRepository;
      Bus = bus;
      _commodityOpenHighLowCloseRepository = commodityOpenHighLowCloseRepository;
      _rateProvider = rateProvider;
    }

    public async Task<bool> Handle(AtualizarOHLCCommoditiesRateCommand message, CancellationToken cancellationToken)
    {
      if (message == null)
      {
        return false;
      }
      var symbols = await _symbolsRepository.ObterSymbolsAppVisible();
      var openingRates = await _rateProvider.GetOpeningRateAsync(DateTime.UtcNow.AddDays(-1).ToString("MM-dd-yyyy"), symbols);

      foreach (var symbol in openingRates)
      {
        var ohlcExisting = await _commodityOpenHighLowCloseRepository.ObterOHCLBySymbolCode(symbol.Symbol);
        if (ohlcExisting is null)
        {
          _commodityOpenHighLowCloseRepository.Add(symbol);
        }
        else if (ohlcExisting is not null)
        {
          ohlcExisting.Base = symbol.Base;
          ohlcExisting.Timestamp = symbol.Timestamp;
          ohlcExisting.Date = symbol.Date;
          ohlcExisting.PriceClose = symbol.PriceClose;
          ohlcExisting.PriceOpen = symbol.PriceOpen;
          ohlcExisting.PriceLow = symbol.PriceLow;
          ohlcExisting.PriceHigh = symbol.PriceHigh;
          _commodityOpenHighLowCloseRepository.Update(ohlcExisting);

        }
      }
      Commit();

      return await Task.FromResult(true);
    }
  }
}
