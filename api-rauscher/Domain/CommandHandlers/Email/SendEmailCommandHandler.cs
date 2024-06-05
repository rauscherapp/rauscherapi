using Domain.Commands;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Interfaces;
using Domain.Options;
using MailKit.Net.Smtp;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
  public class SendEmailCommandHandler : CommandHandler,
          IRequestHandler<EmailCommand, bool>
  {
    private readonly ParametersOptions _parameters;
    private readonly IMediatorHandler Bus;

    public SendEmailCommandHandler(
      IUnitOfWork uow,
      IMediatorHandler bus,
      INotificationHandler<DomainNotification> notifications,
      IOptionsSnapshot<ParametersOptions> parameters) : base(uow, bus, notifications)
    {
      Bus = bus;
      _parameters = parameters.Value;
    }
    public async Task<bool> Handle(EmailCommand message, CancellationToken cancellationToken)
    {
      var emailMessage = new MimeMessage();
      emailMessage.From.Add(new MailboxAddress("no-reply@rauscher.com.br", _parameters.EmailSender));
      emailMessage.To.Add(new MailboxAddress("To", _parameters.EmailReceiver));
      emailMessage.Subject = $"Customer Contact from MobileApp: {message.CustomerEmail}";

      // Constructing the body text
      var formattedBody = $@"
            Email: {message.CustomerEmail}
            Subject: {message.Subject}
            Body:
            {message.Body}
            ";

      var bodyBuilder = new BodyBuilder { TextBody = formattedBody };

      //if (attachments != null)
      //{
      //  foreach (var attachment in attachments)
      //  {
      //    bodyBuilder.Attachments.Add(attachment);
      //  }
      //}

      emailMessage.Body = bodyBuilder.ToMessageBody();

      using (var client = new SmtpClient())
      {
        await client.ConnectAsync(_parameters.SmtpServer, _parameters.SmtpPort, true);
        await client.AuthenticateAsync(_parameters.EmailSender, _parameters.EmailPassword, cancellationToken);
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
      }

      return true;
    }

  }
}
