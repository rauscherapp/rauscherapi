using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarPostQuery : IRequest<PagedList<Post>>
	{
		public PostParameters Parameters { get; internal set; }
		
		public ListarPostQuery(PostParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
