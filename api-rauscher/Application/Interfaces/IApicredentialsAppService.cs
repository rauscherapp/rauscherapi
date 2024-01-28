using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IApiCredentialsAppService : IDisposable
  {
    Task<ApicredentialsViewModel> AtualizarApicredentials(ApicredentialsViewModel apicredentialsViewModel);
    Task<ApicredentialsViewModel> CadastrarApicredentials(ApicredentialsViewModel apicredentialsViewModel);
    Task<bool> ExcluirApicredentials(string apiKey);
    Task<ApicredentialsViewModel> ObterApicredentials(string apiKey);
    Task<PagedResponse<ApicredentialsViewModel>> ListarApicredentials(ApiCredentialsParameters parameters);
    Task<bool> GerarApiCredentials(string document);
  }
}
