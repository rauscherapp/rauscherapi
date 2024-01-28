using Domain.Commands;

namespace Domain.Validations
{
  public class AtualizarSymbolsCommandValidation : SymbolsCommandValidation<AtualizarSymbolsCommand>
  {
    public AtualizarSymbolsCommandValidation()
    {
      ValidateId();
    }
  }
}
