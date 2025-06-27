using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarAppParametersCommand : AppParametersCommand
  {
    public string StripeApiPriceId { get; internal set; }

    public override bool IsValid()
    {
      ValidationResult = new AtualizarAppParametersCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
