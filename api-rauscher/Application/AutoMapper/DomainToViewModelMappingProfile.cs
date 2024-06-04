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
      CreateMap<EventRegistry, EventRegistryViewModel>()
        .ForMember(dest => dest.EventRegistryId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventName))
        .ForMember(dest => dest.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
        .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType))
        .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
        .ForMember(dest => dest.EventLocation, opt => opt.MapFrom(src => src.EventLocation))
        .ForMember(dest => dest.EventLink, opt => opt.MapFrom(src => src.EventLink))
        .ForMember(dest => dest.EventDateMonth, opt => opt.MapFrom(src => src.EventDate.ToString("MMM").ToUpper()))
        .ForMember(dest => dest.EventDateDay, opt => opt.MapFrom(src => src.EventDate.Day.ToString()))
        .ForMember(dest => dest.EventDateHour, opt => opt.MapFrom(src => src.EventDate.ToString("hh:mm tt").ToUpper()))
        .ForMember(dest => dest.EventDateYear, opt => opt.MapFrom(src => src.EventDate.ToString("YYYY")));

      CreateMap<AppParameters, AppParametersViewModel>();
      CreateMap<CommoditiesRate, CommoditiesRateViewModel>();
      CreateMap<Symbols, SymbolsViewModel>();
      CreateMap<ApiCredentials, ApicredentialsViewModel>();
      CreateMap<Post, PostViewModel>();
      CreateMap<Folder, FolderViewModel>();
    }
  }
}
