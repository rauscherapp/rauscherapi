using Application.Helpers;
using Application.Interfaces;
using Application.Services;
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
  public class FolderController : ApiController
  {
    private readonly IFolderAppService _folderAppService;
    private readonly IMediatorHandler _bus;
    private readonly IMapper _mapper;

    public FolderController(
        IFolderAppService folderAppService,
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
    {
      _folderAppService = folderAppService;
      _mapper = mapper;
      _bus = bus;
    }

    [HttpGet("Folder")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> Folder([FromQuery] FolderParameters parameters)
    {
      var folders = await _folderAppService.ListarFolder(parameters);
      Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(folders.PaginationMetadata));
      var result = _mapper.Map<IEnumerable<FolderViewModel>>(folders.Data).ShapeData(parameters.Fields);
      return CreateResponse(result);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("Folder/{id}")]
    [AllowAnonymous]

    public async Task<IActionResult> ObterFolder(Guid id)
    {
      var result = await _folderAppService.ObterFolder(id);
      return CreateResponse(result);
    }


    [HttpPost("CreateFolder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<IActionResult> CreateFolder([FromBody] FolderViewModel folderViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _folderAppService.CadastrarFolder(folderViewModel);
      return CreateResponse(result);
    }
    [HttpPut("UpdateFolder/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateFolder([FromBody] FolderViewModel FolderViewModel)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _folderAppService.AtualizarFolder(FolderViewModel);
      return CreateResponse(result);
    }
    [HttpDelete("DeleteFolder/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteFolder(Guid id)
    {
      if (!IsValidOperation())
      {
        return BadRequest(new
        {
          success = false,
          errors = GetNotificationMessages()
        });
      }
      var result = await _folderAppService.ExcluirFolder(id);
      return CreateResponse(result);
    }
  }
}
