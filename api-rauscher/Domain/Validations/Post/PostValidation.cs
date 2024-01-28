using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class PostCommandValidation<T> : AbstractValidator<T> where T : PostCommand
	{
		protected void ValidateId()
		{
			RuleFor(c => c.ID)
			.NotEmpty().WithMessage("Id Post Vazio");
		}
	}
}
