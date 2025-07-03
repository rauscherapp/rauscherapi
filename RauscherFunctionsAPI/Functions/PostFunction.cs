using Application.Helpers;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Data.YahooFinanceApi.Api.Model;
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

      var queryParameters = req.GetQueryParameterDictionary();
      var bindParameters = new BindParameters();
      var parameters = bindParameters.BindQueryParameters<PostParameters>(queryParameters);

      try
      {
        var posts = await _postAppService.ListarPost(parameters);
        var result = _mapper.Map<IEnumerable<PostViewModel>>(posts.Data).ShapeData(parameters.Fields);

        req.HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(posts.PaginationMetadata));
        return CreateResponseList(posts.PaginationMetadata, result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error listing posts: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("GetPostById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPostById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/Post/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing GET request for Post with ID: {id}");

      try
      {
        var result = await _postAppService.ObterPost(id);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error getting post by ID: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("CreatePost")]
    [AllowAnonymous]
    public async Task<IActionResult> CreatePost(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/CreatePost")] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("Processing POST request to create a new Post.");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var postViewModel = JsonSerializer.Deserialize<PostViewModel>(requestBody);

      try
      {
        var result = await _postAppService.CadastrarPost(postViewModel);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error creating post: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("UpdatePost")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdatePost(
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/UpdatePost/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing PATCH request to update Post with ID: {id}");

      var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var postViewModel = JsonSerializer.Deserialize<PostViewModel>(requestBody);

      try
      {
        var result = await _postAppService.AtualizarPost(postViewModel);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error updating post: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

    [FunctionName("DeletePost")]
    [AllowAnonymous]
    public async Task<IActionResult> DeletePost(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/Post/{id}")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing DELETE request to delete Post with ID: {id}");

      try
      {
        var result = await _postAppService.ExcluirPost(id);
        return CreateResponse(result);
      }
      catch (Exception ex)
      {
        log.LogError($"Error deleting post: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }
    [FunctionName("UploadPostImage")]
    public async Task<IActionResult> UploadPostImage(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/Post/{id}/UploadImage")] HttpRequest req,
        Guid id,
        ILogger log)
    {
      log.LogInformation($"Processing image upload for Post ID: {id}");

      var formCollection = await req.ReadFormAsync();
      var file = formCollection.Files["image"];

      if (file == null)
      {
        return CreateResponse(false);
      }

      try
      {
        var imageUrl = await _postAppService.UploadPostImage(id, file);

        if (imageUrl)
        {
          return CreateResponse(false);
        }

        return CreateResponse(new { ImageUrl = imageUrl });
      }
      catch (Exception ex)
      {
        log.LogError($"Error uploading post image: {ex.Message}");
        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }
    }

  }
}
