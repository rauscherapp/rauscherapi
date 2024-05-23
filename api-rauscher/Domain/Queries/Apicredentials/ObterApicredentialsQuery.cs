using Domain.Models;
using MediatR;

namespace Domain.Queries
{
  public class ObterApiCredentialsQuery : IRequest<ApiCredentials>
  {
    public string ApiKey { get; internal set; }

    public ObterApiCredentialsQuery(string apiKey)
    {
      ApiKey = apiKey;
    }
  }
}
