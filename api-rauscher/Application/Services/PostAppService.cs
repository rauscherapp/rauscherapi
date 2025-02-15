using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Commands;
using Domain.Models;
using Domain.Queries;
using Domain.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Application.Services
{
	public class PostAppService : IPostAppService
	{
		private IEnumerable<PostViewModel> _dataResponse;
		private PostViewModel _response;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger<PostAppService> _logger;
		private readonly IUriAppService _uriAppService;
		
		public PostAppService(ILogger<PostAppService> logger, IMediator mediator, IMapper mapper, IUriAppService uriAppService)
		{
			_mediator = mediator;
			_mapper = mapper;
			_logger = logger;
			_response = new PostViewModel();
			_uriAppService = uriAppService;
		}
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		public async Task<PostViewModel> AtualizarPost(PostViewModel PostViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(AtualizarPost));
			var command = _mapper.Map<AtualizarPostCommand>(PostViewModel);
			await _mediator.Send(command);
			return PostViewModel;
		}
		public async Task<PostViewModel> CadastrarPost(PostViewModel PostViewModel)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(CadastrarPost));
			var command = _mapper.Map<CadastrarPostCommand>(PostViewModel);
			await _mediator.Send(command);
			return PostViewModel;
		}
		public async Task<bool> ExcluirPost(Guid Post)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ExcluirPost));
			var command = new ExcluirPostCommand(Post);
			await _mediator.Send(command);
			return true;
		}
		public async Task<PagedResponse<PostViewModel>> ListarPost(PostParameters parameters)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ListarPost));
			var data = await _mediator.Send(new ListarPostQuery(parameters));
			var resultadoDB = data.Select(x => _mapper.Map<Post, PostViewModel>(x));
			
			var viewModelPagedList = PagedList<PostViewModel>.Create(resultadoDB.AsQueryable(), parameters.PageNumber, parameters.PageSize);
			
			return PaginationHelpers.CreatePaginatedResponse(viewModelPagedList, parameters, "Mostrar", _uriAppService);
		}
		public async Task<PostViewModel> ObterPost(Guid Post)
		{
			_logger.LogInformation("Handling: {MethodName}", nameof(ObterPost));
			var data = await _mediator.Send(new ObterPostQuery(Post));
			var resultadoDB = _mapper.Map<Post, PostViewModel>(data);
			if (resultadoDB == null) return resultadoDB;
			
			return resultadoDB;
		}
    public async Task<bool> UploadPostImage(Guid postId, IFormFile file)
    {
      _logger.LogInformation("Handling: {MethodName}", nameof(UploadPostImage));

      var command = new UploadPostImageCommand(postId, file);
      var result = await _mediator.Send(command);

      return result;
    }

  }
}
