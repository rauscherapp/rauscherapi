using Domain.Validations;

namespace Domain.Commands
{
  public class CadastrarEventRegistryCommand : EventRegistryCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new CadastrarEventRegistryCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
