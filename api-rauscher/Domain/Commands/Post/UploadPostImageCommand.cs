using Domain.Core.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Domain.Commands
{
  public class UploadPostImageCommand : PostCommand
  {
    public UploadPostImageCommand(Guid postId, IFormFile file)
    {
      PostId = postId;
      File = file;
    }

    public Guid PostId { get; }
    public IFormFile File { get; }

    public override bool IsValid()
    {
      return PostId != Guid.Empty && File != null && File.Length > 0;
    }
  }
}
