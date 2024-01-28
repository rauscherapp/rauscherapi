using Domain.Commands.Apicredentials;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.Events;
using Domain.Interfaces;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.Apicredentials
{
  public class GerarSecretAndApiKeyCommandHandler : CommandHandler,
          IRequestHandler<GerarSecretAndApiKeyCommand, bool>
  {
    private readonly IApiCredentialsRepository _apicredentialsRepository;
    private readonly IMediatorHandler Bus;

    public GerarSecretAndApiKeyCommandHandler(IApiCredentialsRepository apicredentialsRepository,
                                                IUnitOfWork uow,
                                                IMediatorHandler bus,
                                                INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
    {
      _apicredentialsRepository = apicredentialsRepository;
      Bus = bus;
    }

    public Task<bool> Handle(GerarSecretAndApiKeyCommand message, CancellationToken cancellationToken)
    {
      var apiKey = GenerateApiKey();
      var apiSecret = GenerateApiSecret();
      var apiSecretHash = HashSecret(apiSecret);

      var apiCredentials = new ApiCredentials(apiKey, apiSecretHash, DateTime.Now, null, true);
      _apicredentialsRepository.Add(apiCredentials);

      if (Commit())
      {
        Bus.RaiseEvent(new CadastrarApicredentialsEvent(
          apiCredentials.Apikey,
          apiCredentials.Apisecrethash,
          apiCredentials.Createdat,
          apiCredentials.Lastusedat,
          apiCredentials.Isactive
          ));
        return Task.FromResult(true);
      }
      return Task.FromResult(true);
    }


    private string GenerateApiKey()
    {
      return Guid.NewGuid().ToString("N"); // The "N" format specifier omits dashes
    }

    private string GenerateApiSecret()
    {
      using (var rng = new RNGCryptoServiceProvider())
      {
        var byteArr = new byte[32]; // 256 bits
        rng.GetBytes(byteArr);

        // Convert to a Base64 string. You can choose other formats if required
        return Convert.ToBase64String(byteArr);
      }
    }

    private string HashSecret(string document)
    {
      using (var sha256 = SHA256.Create())
      {
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(document));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
      }
    }
  }
}
