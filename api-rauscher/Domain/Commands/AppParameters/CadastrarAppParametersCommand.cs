using Domain.Validations;

namespace Domain.Commands
{
  public class CadastrarAppParametersCommand : AppParametersCommand
  {
    public string StripeApiPriceId { get; internal set; }

    public override bool IsValid()
    {
      ValidationResult = new CadastrarAppParametersCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
