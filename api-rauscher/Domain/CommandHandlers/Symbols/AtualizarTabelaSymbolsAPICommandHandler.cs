using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class AtualizarTabelaSymbolsAPICommandHandler : CommandHandler,
          IRequestHandler<AtualizarTabelaSymbolsAPICommand, bool>
  {
    private readonly ISymbolsRepository _symbolsRepository;
    private readonly ICommoditiesRepository _commoditiesRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarTabelaSymbolsAPICommandHandler(ISymbolsRepository symbolsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications,
                                                 ICommoditiesRepository commoditiesRepository) : base(uow, bus, notifications)
    {
      _symbolsRepository = symbolsRepository;
      _commoditiesRepository = commoditiesRepository;
      Bus = bus;      
    }
    public async Task<bool> Handle(AtualizarTabelaSymbolsAPICommand message, CancellationToken cancellationToken)
    {

      var symbolsApi = await _commoditiesRepository.GetSymbolsAsync();


      var getSymbols = new List<string>
      {
        "KCK24",
        "RMH24",
        "SUGAR",
        "SOYBEAN",
        "CORN"
      };


      foreach ( var symbol in symbolsApi)
      {
        var symbols = _symbolsRepository.ObterSymbolsByCode(symbol.Code);
        if (symbols == null)
        {
          _symbolsRepository.Add(symbol);

          if (Commit())
          {
            await Bus.RaiseEvent(new CadastrarSymbolsEvent(
              message.Id,
              message.Code,
              message.Name,
              message.Appvisible,
              message.FriendlyName
              ));
          }
        }
        else
        {
          symbols.Id = symbols.Id;
          symbols.Code = symbols.Code;
          symbols.Name = symbols.Name;
          symbols.Appvisible = symbols.Appvisible;

          _symbolsRepository.Update(symbols);

          if (Commit())
          {
            await Bus.RaiseEvent(new AtualizarSymbolsEvent(
              symbols.Id,
              symbols.Code,
              symbols.Name,
              symbols.Appvisible
              ));
          }
        }
      }
      return true;
    }

  }
}
