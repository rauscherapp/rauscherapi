using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class ExcluirSymbolsCommand : SymbolsCommand
  {
    public Guid ID { get; set; }
    public ExcluirSymbolsCommand(Guid id)
    {
      ID = id;
    }
    public override bool IsValid()
    {
      ValidationResult = new ExcluirSymbolsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
