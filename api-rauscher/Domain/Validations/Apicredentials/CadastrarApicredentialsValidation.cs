using Domain.Commands;

namespace Domain.Validations
{
  public class CadastrarApicredentialsCommandValidation : ApiCredentialsCommandValidation<CadastrarApicredentialsCommand>
  {
    public CadastrarApicredentialsCommandValidation()
    {
      ValidateId();
    }
  }
}
