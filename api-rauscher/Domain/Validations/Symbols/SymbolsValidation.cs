using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
  public class SymbolsCommandValidation<T> : AbstractValidator<T> where T : SymbolsCommand
  {
    protected void ValidateId()
    {
      RuleFor(c => c.Id)
      .NotEmpty().WithMessage("Id Symbols Vazio");
    }
  }
}
