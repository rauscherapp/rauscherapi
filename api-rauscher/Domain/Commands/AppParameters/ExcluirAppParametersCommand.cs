using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class ExcluirAppParametersCommand : AppParametersCommand
  {
    public Guid AppParametersId { get; set; }
    public ExcluirAppParametersCommand(Guid appParametersId)
    {
      AppParametersId = appParametersId;
    }
    public override bool IsValid()
    {
      ValidationResult = new ExcluirAppParametersCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
