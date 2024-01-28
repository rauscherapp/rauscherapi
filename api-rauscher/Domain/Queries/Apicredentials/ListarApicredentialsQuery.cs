using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarApicredentialsQuery : IRequest<PagedList<ApiCredentials>>
	{
		public ApiCredentialsParameters Parameters { get; internal set; }
		
		public ListarApicredentialsQuery(ApiCredentialsParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
