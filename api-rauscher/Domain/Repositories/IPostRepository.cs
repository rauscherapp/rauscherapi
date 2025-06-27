using Domain.Interfaces;
using Domain.Models;
using Domain.QueryParameters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
	public interface IPostRepository : IRepository<Post>
	{
		//Task<PagedList<Post>> ListarPosts(PostParameters parameters);
		Post ObterPost(Guid id);
		Task<IQueryable<Post>> ListarPostsByFolderId(PostParameters parameters, Guid folderId);
    Task<IQueryable<Post>> ListarPosts(PostParameters parameters);
  }
}
