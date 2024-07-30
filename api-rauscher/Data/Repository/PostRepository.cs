using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
	public class PostRepository : Repository<Post>, IPostRepository
	{
		public PostRepository(RauscherDbContext context): base(context)
		{
		}
		public Post ObterPost(Guid id)
		{
			    var Post = Db.Posts
			        .Where(c => c.Id == id);
			
			    return Post.FirstOrDefault();
		}
		
		public async Task<PagedList<Post>> ListarPosts(PostParameters parameters)
		{
			var post = Db.Posts
			.AsQueryable();
			
			if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
			    post = post.ApplySort(parameters.OrderBy);
			
			return PagedList<Post>.Create(post, parameters.PageNumber, parameters.PageSize);
		}
    public async Task<PagedList<Post>> ListarPostsByFolderId(PostParameters parameters, Guid folderId)
    {
      // Assuming Db.Posts is an IQueryable<Post>
      var query = Db.Posts
          .AsNoTracking() // Add this if you're not updating the posts
          .Where(post => post.FolderId == folderId); // No need to check if folderId is null

			if (!parameters.source.Equals("webadmin"))
      {
        query = query.Where(post => post.Visible);
      }

      if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
      {
        query = query.ApplySort(parameters.OrderBy); // Assuming ApplySort is an extension method
      }

      return PagedList<Post>.Create(query, parameters.PageNumber, parameters.PageSize);
    }
  }
}
