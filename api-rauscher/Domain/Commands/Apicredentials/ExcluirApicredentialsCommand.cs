using Domain.Validations;

namespace Domain.Commands
{
  public class ExcluirApicredentialsCommand : ApicredentialsCommand
  {
    public string ApiKey { get; set; }
    public ExcluirApicredentialsCommand(string apiKey)
    {
      ApiKey = apiKey;
    }
    public override bool IsValid()
    {
      ValidationResult = new ExcluirApicredentialsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
