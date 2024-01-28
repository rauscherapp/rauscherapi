using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;

namespace Application.AutoMapper
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public ViewModelToDomainMappingProfile()
    {
      //ConfigureViewModelToDomain 
 CreateMap<CommoditiesRateViewModel, CommoditiesRate>(); 
 CreateMap<CommoditiesRateViewModel, ExcluirCommoditiesRateCommand>(); 
 CreateMap<CommoditiesRateViewModel, CadastrarCommoditiesRateCommand>(); 
 CreateMap<CommoditiesRateViewModel, AtualizarCommoditiesRateCommand>(); 
      CreateMap<SymbolsViewModel, Symbols>();
      CreateMap<SymbolsViewModel, ExcluirSymbolsCommand>();
      CreateMap<SymbolsViewModel, CadastrarSymbolsCommand>();
      CreateMap<SymbolsViewModel, AtualizarSymbolsCommand>();
      CreateMap<SymbolsViewModel, AtualizarTabelaSymbolsAPICommand>();
      CreateMap<ApicredentialsViewModel, ApiCredentials>();
      CreateMap<ApicredentialsViewModel, ExcluirApicredentialsCommand>();
      CreateMap<ApicredentialsViewModel, CadastrarApicredentialsCommand>();
      CreateMap<ApicredentialsViewModel, AtualizarApicredentialsCommand>();
      CreateMap<PostViewModel, Post>();
      CreateMap<PostViewModel, ExcluirPostCommand>();
      CreateMap<PostViewModel, CadastrarPostCommand>();
      CreateMap<PostViewModel, AtualizarPostCommand>();
      CreateMap<FolderViewModel, Folder>();
      CreateMap<FolderViewModel, ExcluirFolderCommand>();
      CreateMap<FolderViewModel, CadastrarFolderCommand>();
      CreateMap<FolderViewModel, AtualizarFolderCommand>();
      CreateMap<FolderViewModel, ExcluirFolderCommand>();
      CreateMap<FolderViewModel, CadastrarFolderCommand>();
      CreateMap<FolderViewModel, AtualizarFolderCommand>();
    }
  }
}
