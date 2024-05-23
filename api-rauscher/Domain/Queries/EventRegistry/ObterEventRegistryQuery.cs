using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System;
using System.Linq;

namespace Domain.Queries
{
	public class ObterEventRegistryQuery : IRequest<EventRegistry>
	{
		public Guid EventRegistryId { get; internal set; }
		
		public ObterEventRegistryQuery(Guid eventRegistryId)
		{
			EventRegistryId = eventRegistryId;
		}
	}
}
