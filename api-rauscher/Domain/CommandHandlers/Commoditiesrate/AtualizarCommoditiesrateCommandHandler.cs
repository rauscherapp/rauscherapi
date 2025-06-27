using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class AtualizarCommoditiesRateCommandHandler : CommandHandler,
          IRequestHandler<AtualizarCommoditiesRateCommand, bool>
  {
    private readonly ICommoditiesRateRepository _CommoditiesRateRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarCommoditiesRateCommandHandler(ICommoditiesRateRepository CommoditiesRateRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _CommoditiesRateRepository = CommoditiesRateRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarCommoditiesRateCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var CommoditiesRate = _CommoditiesRateRepository.GetById(message.Id);
      CommoditiesRate.Id = message.Id;
      CommoditiesRate.Timestamp = message.Timestamp;
      CommoditiesRate.BaseCurrency = message.Base;
      CommoditiesRate.Date = message.Date;
      CommoditiesRate.SymbolCode = message.Code;
      CommoditiesRate.Unit = message.Unit;
      CommoditiesRate.Price = message.Price;
      CommoditiesRate.Variationprice = message.Variationprice;
      CommoditiesRate.Variationpricepercent = message.Variationpricepercent;
      CommoditiesRate.Isup = message.Isup;

      _CommoditiesRateRepository.Update(CommoditiesRate);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarCommoditiesRateEvent(
          message.Id,
          message.Timestamp,
          message.Base,
          message.Date,
          message.Code,
          message.Unit,
          message.Price,
          message.Variationprice,
          message.Variationpricepercent,
          message.Isup
          ));
      }
      return Task.FromResult(true);
    }

  }
}
