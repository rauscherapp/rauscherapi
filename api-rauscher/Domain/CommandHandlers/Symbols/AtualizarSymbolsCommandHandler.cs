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
  public class AtualizarSymbolsCommandHandler : CommandHandler,
          IRequestHandler<AtualizarSymbolsCommand, bool>
  {
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarSymbolsCommandHandler(ISymbolsRepository symbolsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _symbolsRepository = symbolsRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarSymbolsCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var symbols = _symbolsRepository.GetById(message.Id);
      symbols.Id = message.Id;
      symbols.Code = message.Code;
      symbols.Name = message.Name;
      symbols.FriendlyName = message.FriendlyName;
      symbols.SymbolType = message.SymbolType;
      symbols.Appvisible = message.Appvisible;
      symbols.Vendor = message.Vendor;

      _symbolsRepository.Update(symbols);

      if (Commit())
      {
        Bus.RaiseEvent(new AtualizarSymbolsEvent(
          message.Id,
          message.Code,
          message.Name,
          message.FriendlyName,
          message.SymbolType,
          message.Appvisible
          ));
      }

      return Task.FromResult(true);
    }

  }
}
