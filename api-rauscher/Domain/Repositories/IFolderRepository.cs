using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IFolderRepository : IRepository<Folder>
	{
		Task<PagedList<Folder>> GetFolders(FolderParameters parameters);
		Folder GetFolder(Guid idFolder);
		Folder GetFoldersBySlug(string slug);

  }
}
