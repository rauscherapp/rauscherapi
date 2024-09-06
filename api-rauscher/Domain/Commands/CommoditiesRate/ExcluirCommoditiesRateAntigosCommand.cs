using Domain.Core.Commands;
using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class ExcluirCommoditiesRateAntigosCommand : CommoditiesRateCommand
  {
    public DateTime DataLimite { get; private set; }

    public ExcluirCommoditiesRateAntigosCommand()
    {
      DataLimite = DateTime.UtcNow.AddDays(-1); // Define 7 dias atrás como a data limite
    }
    public override bool IsValid()
    {
      return true;
    }
  }
}