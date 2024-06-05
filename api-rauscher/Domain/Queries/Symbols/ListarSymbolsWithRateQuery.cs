using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarSymbolsWithRateQuery : IRequest<PagedList<Symbols>>
	{
		public SymbolsParameters Parameters { get; internal set; }
		
		public ListarSymbolsWithRateQuery(SymbolsParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
