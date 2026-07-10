using Data.Context;
using Domain.Models;
using Domain.QueryParameters;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(RauscherDbContext context) : base(context)
        {
        }
        public async Task<Post> ObterPost(Guid id)
        {
            return Db.Posts
                     .AsTracking()
                     .FirstOrDefault(c => c.Id == id);
        }

        public async Task<IQueryable<Post>> ListarPosts(PostParameters parameters)
        {
            var post = Db.Posts
            .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.language))
                post = post.Where(x => x.Language.Equals(parameters.language));

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
                post = post.ApplySort(parameters.OrderBy);

            return post.AsQueryable();
        }
        public async Task<IQueryable<Post>> ListarPostsByFolderId(PostParameters parameters, Guid folderId)
        {
            // Assuming Db.Posts is an IQueryable<Post>
            var query = Db.Posts
                .AsNoTracking() // Add this if you're not updating the posts
                .Where(post => post.FolderId == folderId); // No need to check if folderId is null

            if (!parameters.source.Equals("webadmin"))
            {
                parameters.OrderBy = "createddate desc";
                query = query.Where(post => post.Visible);
            }

            if (!string.IsNullOrEmpty(parameters.language))
                query = query.Where(x => x.Language.Equals(parameters.language));

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                if (!parameters.SearchQuery.Equals("null"))
                {
                    var searchQuery = parameters.SearchQuery.ToLower();
                    query = query.Where(s => s.Title.ToLower().Contains(searchQuery)
                                               || s.Language.ToLower().Contains(searchQuery)
                                               || s.Content.ToLower().Contains(searchQuery));
                }
            }
            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                query = query.ApplySort(parameters.OrderBy); // Assuming ApplySort is an extension method
            }

            return query.AsQueryable();
        }
    }
}
