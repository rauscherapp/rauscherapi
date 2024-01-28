using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	    public interface IFolderAppService : IDisposable
	{
		Task<FolderViewModel> AtualizarFolder(FolderViewModel folderViewModel);
		Task<FolderViewModel> CadastrarFolder(FolderViewModel folderViewModel);
		Task<bool> ExcluirFolder(Guid cdFolder);
    Task<FolderViewModel> ObterFolder(Guid cdVenda);
		Task<PagedResponse<FolderViewModel>> ListarFolder(FolderParameters parameters);
	}
}
