using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarEventRegistryQuery : IRequest<PagedList<EventRegistry>>
	{
		public EventRegistryParameters Parameters { get; internal set; }
		
		public ListarEventRegistryQuery(EventRegistryParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
