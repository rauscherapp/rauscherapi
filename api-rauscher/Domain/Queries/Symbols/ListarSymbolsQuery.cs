using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarSymbolsQuery : IRequest<PagedList<Symbols>>
	{
		public SymbolsParameters Parameters { get; internal set; }
		
		public ListarSymbolsQuery(SymbolsParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
