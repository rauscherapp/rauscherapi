using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class ExcluirEventRegistryCommand : EventRegistryCommand
  {
    public Guid EventRegistryId { get; set; }
    public ExcluirEventRegistryCommand(Guid eventRegistryId)
    {
      EventRegistryId = eventRegistryId;
    }
    public override bool IsValid()
    {
      ValidationResult = new ExcluirEventRegistryCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
