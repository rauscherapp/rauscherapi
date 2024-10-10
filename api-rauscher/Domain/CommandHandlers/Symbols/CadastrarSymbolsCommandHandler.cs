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
  public class CadastrarSymbolsCommandHandler : CommandHandler,
          IRequestHandler<CadastrarSymbolsCommand, bool>
  {
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly IMediatorHandler Bus;

    public CadastrarSymbolsCommandHandler(ISymbolsRepository symbolsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _symbolsRepository = symbolsRepository;
      Bus = bus;
    }
    public Task<bool> Handle(CadastrarSymbolsCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var symbols = new Symbols(message.Code,
        message.Name,
        message.Appvisible,
        message.FriendlyName,
        message.SymbolType,
        message.Vendor
        );

      _symbolsRepository.Add(symbols);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarSymbolsEvent(message.Id,
          message.Code,
          message.Name,
          message.Appvisible,
          message.FriendlyName
          ));
      }

      return Task.FromResult(true);
    }

  }
}
