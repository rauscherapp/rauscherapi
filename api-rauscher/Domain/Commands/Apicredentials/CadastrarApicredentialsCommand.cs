using Domain.Validations;

namespace Domain.Commands
{
  public class CadastrarApicredentialsCommand : ApicredentialsCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new CadastrarApicredentialsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
