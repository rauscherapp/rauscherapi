using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarEventRegistryCommand : EventRegistryCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new AtualizarEventRegistryCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
