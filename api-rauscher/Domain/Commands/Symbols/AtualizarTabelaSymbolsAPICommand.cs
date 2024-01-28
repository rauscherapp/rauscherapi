using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarTabelaSymbolsAPICommand : SymbolsCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new AtualizarTabelaSymbolsAPIValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
