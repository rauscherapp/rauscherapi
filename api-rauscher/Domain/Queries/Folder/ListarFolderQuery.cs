using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System.Linq;

namespace Domain.Queries
{
	public class ListarFolderQuery : IRequest<PagedList<Folder>>
	{
		public FolderParameters Parameters { get; internal set; }
		
		public ListarFolderQuery(FolderParameters parameters)
		{
			Parameters = parameters;
		}
	}
}
