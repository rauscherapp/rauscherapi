using Domain.Validations;
using System;

namespace Domain.Commands
{
  public class ExcluirCommoditiesRateCommand : CommoditiesRateCommand
  {
    public Guid id { get; set; }
    public ExcluirCommoditiesRateCommand(Guid id)
    {
      Id = id;
    }
    public override bool IsValid()
    {
      ValidationResult = new ExcluirCommoditiesRateCommandValidation().Validate(this);
      return ValidationResult.IsValid;
    }
  }
}
