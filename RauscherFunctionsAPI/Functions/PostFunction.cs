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
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RauscherFunctionsAPI
{
  public class PostFunction : BaseFunctions
  {
    private readonly IPostAppService _postAppService;
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _bus;

    public PostFunction(
        IPostAppService postAppService,
        IMediatorHandler bus,
        IMapper mapper,
        INotificationHandler<DomainNotification> notifications) : base(notifications, bus)
    {
      _postAppService = postAppService;
      _bus = bus;
      _mapper = mapper;
    }

    [FunctionName("GetPosts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPosts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Post")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing GET request for Posts.");

      var parameters = new PostParameters();
      // Optionally: parse query parameters from request to populate 'parameters'

      var posts = await _postAppService.ListarPost(parameters);
      var result = _mapper.Map<IEnumerable<PostViewModel>>(posts.Data).ShapeData(parameters.Fields);

      req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(posts.PaginationMetadata));
      return CreateResponse(result);
    }

    [FunctionName("GetPostById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Post/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing GET request for Post with ID: {id}");

      var result = await _postAppService.ObterPost(id);
      return CreateResponse(result);
    }

    [FunctionName("CreatePost")]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePost(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/Post")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request to create a new Post.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var postViewModel = JsonSerializer.Deserialize<PostViewModel>(requestBody);

      var result = await _postAppService.CadastrarPost(postViewModel);
      return CreateResponse(result);
    }

    [FunctionName("UpdatePost")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdatePost(
        [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "v1/Post/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing PATCH request to update Post with ID: {id}");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var postViewModel = JsonSerializer.Deserialize<PostViewModel>(requestBody);

      var result = await _postAppService.AtualizarPost(postViewModel);
      return CreateResponse(result);
    }

    [FunctionName("DeletePost")]
    [AllowAnonymous]
    public async Task<IActionResult> DeletePost(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "v1/Post/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing DELETE request to delete Post with ID: {id}");

      var result = await _postAppService.ExcluirPost(id);
      return CreateResponse(result);
    }
  }
}
