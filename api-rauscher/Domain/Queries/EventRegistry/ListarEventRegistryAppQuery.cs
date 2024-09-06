using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarEventRegistryAppQuery : IRequest<PagedList<EventRegistry>>
	{
		public EventRegistryParameters Parameters { get; internal set; }
		
		public ListarEventRegistryAppQuery(EventRegistryParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
