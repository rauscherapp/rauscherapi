using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class ExcluirCommoditiesRateCommandHandler : CommandHandler,
          IRequestHandler<ExcluirCommoditiesRateCommand, bool>
  {
    private readonly ICommoditiesRateRepository _CommoditiesRateRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirCommoditiesRateCommandHandler(ICommoditiesRateRepository CommoditiesRateRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _CommoditiesRateRepository = CommoditiesRateRepository;
      Bus = bus;
    }
    public Task<bool> Handle(ExcluirCommoditiesRateCommand message, CancellationToken cancellationToken)
    {
      _CommoditiesRateRepository.Remove(message.Id);

      if (Commit())
      {
        Bus.RaiseEvent(new ExcluirCommoditiesRateEvent(
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
