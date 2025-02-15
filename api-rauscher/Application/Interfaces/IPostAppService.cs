using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	    public interface IPostAppService : IDisposable
	{
		Task<PostViewModel> AtualizarPost(PostViewModel postViewModel);
		Task<PostViewModel> CadastrarPost(PostViewModel postViewModel);
		Task<bool> ExcluirPost(Guid Post);
		Task<PostViewModel> ObterPost(Guid Post);
		Task<PagedResponse<PostViewModel>> ListarPost(PostParameters parameters);
    Task<bool> UploadPostImage(Guid postId, IFormFile file);
  }
}
