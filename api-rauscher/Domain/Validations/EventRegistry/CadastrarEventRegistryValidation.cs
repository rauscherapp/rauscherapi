using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class CadastrarEventRegistryCommandValidation : EventRegistryCommandValidation<CadastrarEventRegistryCommand>
	{
		public CadastrarEventRegistryCommandValidation()
		{
			    ValidateEventRegistryId();
		}
	}
}
