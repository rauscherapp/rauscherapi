using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class ExcluirCommoditiesRateAntigosCommandHandler : CommandHandler,
          IRequestHandler<ExcluirCommoditiesRateAntigosCommand, bool>
  {
    private readonly ICommoditiesRateRepository _CommoditiesRateRepository;
    private readonly IMediatorHandler Bus;

    public ExcluirCommoditiesRateAntigosCommandHandler(ICommoditiesRateRepository CommoditiesRateRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _CommoditiesRateRepository = CommoditiesRateRepository;
      Bus = bus;
    }

    public async Task<bool> Handle(ExcluirCommoditiesRateAntigosCommand message, CancellationToken cancellationToken)
    {
      // Remove registros mais antigos que 7 dias
      await _CommoditiesRateRepository.RemoveOlderThanAsync(message.DataLimite);

      if (Commit())
      {
        // Opcional: Lançar evento após exclusão
        await Bus.RaiseEvent(new ExcluirCommoditiesRateEvent(
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

      return await Task.FromResult(true);
    }
  }
}
