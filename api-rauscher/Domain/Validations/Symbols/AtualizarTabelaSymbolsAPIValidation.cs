using Domain.Commands;

namespace Domain.Validations
{
  public class AtualizarTabelaSymbolsAPIValidation : SymbolsCommandValidation<AtualizarTabelaSymbolsAPICommand>
  {
    public AtualizarTabelaSymbolsAPIValidation()
    {
      ValidateId();
    }
  }
}

