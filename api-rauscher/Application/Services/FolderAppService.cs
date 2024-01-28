using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
	public class FolderAppService : IFolderAppService
	{
		private IEnumerable<FolderViewModel> _dataResponse;
		private FolderViewModel _response;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger<FolderAppService> _logger;
		private readonly IUriAppService _uriAppService;
		
		public FolderAppService(ILogger<FolderAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
		{
			_mediator = mediator;
			_mapper = mapper;
			_logger = logger;
			_response = new FolderViewModel();
			_uriAppService = uriAppService;
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		public async Task<FolderViewModel> AtualizarFolder(FolderViewModel FolderViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(AtualizarFolder));
			var command = _mapper.Map<AtualizarFolderCommand>(FolderViewModel);
			await _mediator.Send(command);
			return FolderViewModel;
		}
		public async Task<FolderViewModel> CadastrarFolder(FolderViewModel FolderViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(CadastrarFolder));
			var command = _mapper.Map<CadastrarFolderCommand>(FolderViewModel);
			await _mediator.Send(command);
			return FolderViewModel;
		}
		public async Task<bool> ExcluirFolder(Guid cdFolder)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ExcluirFolder));
			var command = new ExcluirFolderCommand(cdFolder);
			await _mediator.Send(command);
			return true;
		}
		public async Task<PagedResponse<FolderViewModel>> ListarFolder(FolderParameters parameters)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ListarFolder));
			var data = await _mediator.Send(new ListarFolderQuery(parameters));
			var resultadoDB = data.Select(x => _mapper.Map<Folder, FolderViewModel>(x));
			
			var viewModelPagedList = PagedList<FolderViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);
			
			return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
		}
		public async Task<FolderViewModel> ObterFolder(Guid cdFolder)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ObterFolder));
			var data = await _mediator.Send(new ObterFolderQuery(cdFolder));
			var resultadoDB = _mapper.Map<Folder, FolderViewModel>(data);
			if (resultadoDB == null) return resultadoDB;
			
			return resultadoDB;
		}
	}
}
