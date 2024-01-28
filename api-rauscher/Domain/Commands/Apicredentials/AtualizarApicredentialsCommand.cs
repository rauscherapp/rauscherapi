using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarApicredentialsCommand : ApicredentialsCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new AtualizarApicredentialsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
