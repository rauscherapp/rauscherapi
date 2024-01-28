using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarCommoditiesRateQuery : IRequest<PagedList<CommoditiesRate>>
	{
		public CommoditiesRateParameters Parameters { get; internal set; }
		
		public ListarCommoditiesRateQuery(CommoditiesRateParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
