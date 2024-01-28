using Domain.Models;
using Domain.QueryParameters;
using MediatR;
using System;
using System.Linq;

namespace Domain.Queries
{
	public class ObterFolderQuery : IRequest<Folder>
	{
		public Guid IDFolder { get; internal set; }
		
		public ObterFolderQuery(Guid idFolder)
		{
      IDFolder = idFolder;
		}
	}
}
