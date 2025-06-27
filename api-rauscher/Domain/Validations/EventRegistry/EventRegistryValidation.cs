using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class EventRegistryCommandValidation<T> : AbstractValidator<T> where T : EventRegistryCommand
	{
		protected void ValidateEventRegistryId()
		{
			RuleFor(c => c.EventRegistryId)
			.NotEmpty().WithMessage("Codigo EventRegistry Vazio");
		}
	}
}
