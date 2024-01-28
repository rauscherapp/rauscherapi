using Domain.Commands;
using Domain.Commands.Apicredentials;

namespace Domain.Validations
{
  public class GerarApicredentialsCommandValidation : ApiGenerateCredentialsCommandValidation<GenerateApiCredentialsCommand>
  {
    public GerarApicredentialsCommandValidation()
    {
      ValidateDocument();
    }
  }
}
