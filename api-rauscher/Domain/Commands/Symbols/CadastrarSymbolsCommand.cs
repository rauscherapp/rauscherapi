using Domain.Validations;

namespace Domain.Commands
{
  public class CadastrarSymbolsCommand : SymbolsCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new CadastrarSymbolsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
