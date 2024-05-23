using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirEventRegistryCommandValidation : EventRegistryCommandValidation<ExcluirEventRegistryCommand>
	{
		public ExcluirEventRegistryCommandValidation()
		{
			    ValidateEventRegistryId();
		}
	}
}
