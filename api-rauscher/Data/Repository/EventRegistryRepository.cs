using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class EventRegistryRepository : Repository<EventRegistry>, IEventRegistryRepository
	{
		public EventRegistryRepository(RauscherDbContext context): base(context)
		{
		}
		public EventRegistry ObterEventRegistry(Guid eventRegistryId)
		{
			    var EventRegistry = Db.EventRegistrys
			        .Where(c => c.EventRegistryId == eventRegistryId);
			
			    return EventRegistry.FirstOrDefault();
		}
		
		public async Task<PagedList<EventRegistry>> ListarEventRegistrys(EventRegistryParameters parameters)
		{
			var eventregistry = Db.EventRegistrys
			.AsQueryable();
			
			if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
			    eventregistry = eventregistry.ApplySort(parameters.OrderBy);
			
			return PagedList<EventRegistry>.Create(eventregistry, parameters.PageNumber, parameters.PageSize);
		}
	}
}
