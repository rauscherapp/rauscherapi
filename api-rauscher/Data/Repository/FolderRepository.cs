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
	public class FolderRepository : Repository<Folder>, IFolderRepository
	{
		public FolderRepository(RauscherDbContext context): base(context)
		{
		}
		public Folder GetFolder(Guid idFolder)
		{
			    var Folder = Db.Folders
			        .Where(c => c.ID == idFolder);
			
			    return Folder.FirstOrDefault();
		}

		public async Task<PagedList<Folder>> GetFolders(FolderParameters parameters)
		{
			var folder = Db.Folders
			.AsQueryable();

			if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
				folder = folder.ApplySort(parameters.OrderBy);

			return PagedList<Folder>.Create(folder, parameters.PageNumber, parameters.PageSize);
		}

		public Folder GetFoldersBySlug(string slug)
		{
			var folder = Db.Folders
				.Where(folder => folder.SLUG == slug)
			.AsQueryable();
			
			
			return folder.FirstOrDefault();
    }
  }
}
