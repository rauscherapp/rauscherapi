using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ObterApicredentialsQuery : IRequest<ApiCredentials>
	{
		public string ApiKey { get; internal set; }
		
		public ObterApicredentialsQuery(string apiKey)
		{
      ApiKey = apiKey;
		}
	}
}
