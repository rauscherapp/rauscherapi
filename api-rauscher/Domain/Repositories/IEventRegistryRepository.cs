using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IEventRegistryRepository : IRepository<EventRegistry>
	{
		Task<PagedList<EventRegistry>> ListarEventRegistrys(EventRegistryParameters parameters);
		EventRegistry ObterEventRegistry(Guid eventRegistryId);
	}
}
