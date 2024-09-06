using Domain.Validations;

namespace Domain.Commands
{
  public class AtualizarOHLCCommoditiesRateCommand : CommoditiesRateCommand
  {
    public override bool IsValid()
    {
      return true;
    }
  }
}
