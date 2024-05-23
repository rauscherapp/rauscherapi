using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class AtualizarEventRegistryCommandValidation : EventRegistryCommandValidation<AtualizarEventRegistryCommand>
	{
		public AtualizarEventRegistryCommandValidation()
		{
			    ValidateEventRegistryId();
		}
	}
}
