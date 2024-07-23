using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarAboutUsCommand : AboutUsCommand
  {
    public override bool IsValid()
    {      
      return true;
    }
  }
}
