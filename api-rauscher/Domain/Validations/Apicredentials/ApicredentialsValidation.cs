using Domain.Commands;
using Domain.Commands.Apicredentials;
using FluentValidation;

namespace Domain.Validations
{
  public class ApiCredentialsCommandValidation<T> : AbstractValidator<T> where T : ApicredentialsCommand
  {
    protected void ValidateId()
    {
      RuleFor(c => c.Apikey)
      .NotEmpty().WithMessage("Id Apicredentials Vazio");
    }
  }

  public class ApiGenerateCredentialsCommandValidation<T> : AbstractValidator<T> where T : GenerateApiCredentialsCommand
  {
    protected void ValidateDocument()
    {
      RuleFor(c => c.Document)
      .NotEmpty().WithMessage("Empty Document");
    }
  }
}
