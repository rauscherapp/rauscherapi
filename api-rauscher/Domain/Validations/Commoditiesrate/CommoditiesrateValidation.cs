using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
  public class CommoditiesRateCommandValidation<T> : AbstractValidator<T> where T : CommoditiesRateCommand
  {
    protected void ValidateId()
    {
      RuleFor(c => c.Id)
      .NotEmpty().WithMessage("Id CommoditiesRate Vazio");
    }
  }
}
