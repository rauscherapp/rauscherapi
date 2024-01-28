using Domain.Validations;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Domain.Commands.Apicredentials
{
  public class GerarSecretAndApiKeyCommand : GenerateApiCredentialsCommand
  {
    public GerarSecretAndApiKeyCommand(string document)
    {
      Document = document;
    }
    public override bool IsValid()
    {
      ValidationResult = new GerarApicredentialsCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
