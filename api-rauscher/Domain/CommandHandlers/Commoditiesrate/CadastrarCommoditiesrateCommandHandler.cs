using Domain.Adapters.Providers;
using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using MediatR;
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
    private readonly ICommodityOpenHighLowCloseRepository _ohlcRepository;
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly IAppParametersRepository _appParameters;
    private readonly IRateProvider _rateProvider;
    private readonly IMediatorHandler Bus;

    public CadastrarCommoditiesRateCommandHandler(ICommoditiesRateRepository commoditiesRateRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications,
                                                 ISymbolsRepository symbolsRepository,
                                                 IAppParametersRepository appParameters,
                                                 IRateProvider rateProvider,
                                                 ICommodityOpenHighLowCloseRepository ohlcRepository) : base(uow, bus, notifications)
    {
      _commoditiesRateRepository = commoditiesRateRepository;
      Bus = bus;
      _symbolsRepository = symbolsRepository;
      _appParameters = appParameters;
      _rateProvider = rateProvider;
      _ohlcRepository = ohlcRepository;
    }

    public async Task<bool> Handle(CadastrarCommoditiesRateCommand message, CancellationToken cancellationToken)
    {
      List<CommoditiesRate> resultMapped = new List<CommoditiesRate>();

      var parameters = (await _appParameters.GetAllAsync()).FirstOrDefault();

      var baseSymbols = await _symbolsRepository.ObterSymbolsAppVisible();

      var rates = await _rateProvider.GetRateAsync(baseSymbols);

      foreach (var result in rates)
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

        //var lastRate = await _commoditiesRateRepository.GetLastPriceBeforeTimestamp(result.SymbolCode, result.Timestamp);
        var openPrice = await _ohlcRepository.ObterOHCLBySymbolCode(result.SymbolCode);

        if (openPrice is not null)
        {
          commoditiesRate.CalculateVariation(openPrice.PriceClose);
        }
        else
        {
          commoditiesRate.SetZerosVariation();
        }
        _commoditiesRateRepository.Add(commoditiesRate);
      }
      Commit();
      return true;
    }

  }
}
