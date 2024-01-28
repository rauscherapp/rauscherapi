using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarSymbolsCommand : SymbolsCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new AtualizarSymbolsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
