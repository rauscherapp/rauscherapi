using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	    public interface IAppParametersAppService : IDisposable
	{
		Task<AppParametersViewModel> AtualizarAppParameters(AppParametersViewModel appparametersViewModel);
		Task<AppParametersViewModel> CadastrarAppParameters(AppParametersViewModel appparametersViewModel);
		Task<bool> ExcluirAppParameters(Guid appParametersId);
		Task<AppParametersViewModel> ObterAppParameters(Guid appParametersId);
		Task<PagedResponse<AppParametersViewModel>> ListarAppParameters(AppParametersParameters parameters);
	}
}
