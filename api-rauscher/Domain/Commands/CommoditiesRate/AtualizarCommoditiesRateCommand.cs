using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarCommoditiesRateCommand : CommoditiesRateCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new AtualizarCommoditiesRateCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
