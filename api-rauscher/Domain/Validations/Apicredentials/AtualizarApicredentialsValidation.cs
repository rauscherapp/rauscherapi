using Domain.Commands;

namespace Domain.Validations
{
  public class AtualizarApicredentialsCommandValidation : ApiCredentialsCommandValidation<AtualizarApicredentialsCommand>
  {
    public AtualizarApicredentialsCommandValidation()
    {
      ValidateId();
    }
  }
}
