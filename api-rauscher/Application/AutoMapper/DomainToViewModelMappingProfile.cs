using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public DomainToViewModelMappingProfile()
    {
      //ConfigureDomainToViewModel 
      CreateMap<EventRegistry, EventRegistryViewModel>();
      CreateMap<AppParameters, AppParametersViewModel>();
      CreateMap<CommoditiesRate, CommoditiesRateViewModel>();
      CreateMap<Symbols, SymbolsViewModel>();
      CreateMap<ApiCredentials, ApicredentialsViewModel>();
      CreateMap<Post, PostViewModel>();
      CreateMap<Folder, FolderViewModel>();
    }
  }
}
