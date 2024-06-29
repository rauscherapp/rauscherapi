using Domain.Validations;

namespace Domain.Commands
{
  public class CadastrarPostCommand : PostCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new CadastrarPostCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
