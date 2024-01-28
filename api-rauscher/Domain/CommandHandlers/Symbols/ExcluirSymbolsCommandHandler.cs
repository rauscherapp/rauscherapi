using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Models;
using Domain.Interfaces;
using Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
	    public class ExcluirSymbolsCommandHandler : CommandHandler,
	        IRequestHandler<ExcluirSymbolsCommand, bool>
	{
		        private readonly ISymbolsRepository _symbolsRepository;
		        private readonly IMediatorHandler Bus;
		
		        public ExcluirSymbolsCommandHandler(ISymbolsRepository symbolsRepository,
		                                                     IUnitOfWork uow,
		                                                     IMediatorHandler bus,
		                                                     INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
		{
			            _symbolsRepository = symbolsRepository;
			            Bus = bus;
		}
		        public Task<bool> Handle(ExcluirSymbolsCommand message, CancellationToken cancellationToken)
		{
			_symbolsRepository.Remove(message.ID);
			
			if (Commit())
			{
				                Bus.RaiseEvent(new ExcluirSymbolsEvent(
				message.Id,
				message.Code,
				message.Name,
				message.Appvisible
				));
			}
			return Task.FromResult(true);
		}
		
	}
}
