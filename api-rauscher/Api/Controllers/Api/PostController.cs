using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Core.Bus;
using Domain.Core.Notifications;
using Domain.QueryParameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/v{version:apiVersion}")]
  [Produces("application/json", "application/xml")]
  [Consumes("application/json", "application/xml")]
  [AllowAnonymous]
  public class PostController : ApiController
  {
    private readonly IPropertyCheckerService _propertyCheckerService;
    private readonly IPostAppService _postAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public PostController(
        IPropertyCheckerService propertyCheckerService,
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications,
        IPostAppService postAppService) : base(notifications, bus)
    {
      _propertyCheckerService = propertyCheckerService;
      _mapper = mapper;
      _bus = bus;
      _postAppService = postAppService;
    }
    [HttpGet("Post")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromQuery] PostParameters parameters)
    {
      var posts = await _postAppService.ListarPost(parameters);

      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(posts.PaginationMetadata));
      var result = _mapper.Map<IEnumerable<PostViewModel>>(posts.Data).ShapeData(parameters.Fields);
      return CreateResponse(result);
    }


    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("Post/{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> ObterFolder(Guid id)
    {
      var result = await _postAppService.ObterPost(id);
      return CreateResponse(result);
    }

    [HttpPost("CreatePost")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePost([FromBody] PostViewModel postViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _postAppService.CadastrarPost(postViewModel);
      return CreateResponse(result);
    }
    [HttpPatch("UpdatePost/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdatePost([FromBody] PostViewModel PostViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _postAppService.AtualizarPost(PostViewModel);
      return CreateResponse(result);
    }
    [HttpDelete("DeletePost/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> DeletePost(Guid id)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _postAppService.ExcluirPost(id);
      return CreateResponse(result);
    }

    [HttpDelete("Post/{id}/Image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> DeletePostImage(Guid id)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }

      var result = await _postAppService.DeletePostImage(id);
      return CreateResponse(result);
    }
  }
}
