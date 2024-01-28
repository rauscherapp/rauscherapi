using Domain.Validations;

namespace Domain.Commands
{
  public class CadastrarCommoditiesRateCommand : CommoditiesRateCommand
  {
    public override bool IsValid()
    {
      ValidationResult = new CadastrarCommoditiesRateCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
