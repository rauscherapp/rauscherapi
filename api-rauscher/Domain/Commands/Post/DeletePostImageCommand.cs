using Domain.Core.Commands;
using System;

namespace Domain.Commands
{
  public class DeletePostImageCommand : PostCommand
  {
    public DeletePostImageCommand(Guid postId)
    {
      PostId = postId;
    }

    public Guid PostId { get; }

    public override bool IsValid()
    {
      return PostId != Guid.Empty;
    }
  }
}
