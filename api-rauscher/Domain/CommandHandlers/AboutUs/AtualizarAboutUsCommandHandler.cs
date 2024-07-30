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
  public class AtualizarAboutUsCommandHandler : CommandHandler,
          IRequestHandler<AtualizarAboutUsCommand, bool>
  {
    private readonly IAboutUsRepository _aboutUsRepository;
    private readonly IMediatorHandler Bus;

    public AtualizarAboutUsCommandHandler(IAboutUsRepository aboutUsRepository,
                                                 IUnitOfWork uow,
                                                 IMediatorHandler bus,
                                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _aboutUsRepository = aboutUsRepository;
      Bus = bus;
    }
    public Task<bool> Handle(AtualizarAboutUsCommand message, CancellationToken cancellationToken)
    {
      if (!message.IsValid())
      {
        NotifyValidationErrors(message);
        return Task.FromResult(false);
      }
      var aboutUs = _aboutUsRepository.ObterAboutUs();
      aboutUs.Description = message.Description;

      _aboutUsRepository.Update(aboutUs);

      Commit();

      return Task.FromResult(true);
    }

  }
}
