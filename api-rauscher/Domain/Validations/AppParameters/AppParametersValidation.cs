using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
  public class AppParametersCommandValidation<T> : AbstractValidator<T> where T : AppParametersCommand
  {
    protected void ValidateAppParametersId()
    {
      RuleFor(c => c.Id)
      .NotEmpty().WithMessage("Codigo AppParameters Vazio");
    }
  }
}
