using Application.Helpers;
using Application.ViewModels;
using Domain.QueryParameters;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
  public interface IEventRegistryAppService : IDisposable
  {
    Task<EventRegistryViewModel> AtualizarEventRegistry(EventRegistryViewModel eventregistryViewModel);
    Task<EventRegistryViewModel> CadastrarEventRegistry(EventRegistryViewModel eventregistryViewModel);
    Task<bool> ExcluirEventRegistry(Guid eventRegistryId);
    Task<EventRegistryViewModel> ObterEventRegistry(Guid eventRegistryId);
    Task<PagedResponse<EventRegistryViewModel>> ListarEventRegistry(EventRegistryParameters parameters);
  }
}
