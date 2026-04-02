using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class DeletePostImageCommand : PostCommand
  {
    public DeletePostImageCommand(Guid id)
    {
      ID = id;
      PostId = id;
    }

    public Guid PostId { get; }

    public override bool IsValid()
    {
      ValidationResult = new DeletePostImageCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
