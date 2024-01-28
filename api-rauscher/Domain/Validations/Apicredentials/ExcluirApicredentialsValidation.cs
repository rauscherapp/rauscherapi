using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirApicredentialsCommandValidation : ApiCredentialsCommandValidation<ExcluirApicredentialsCommand>
	{
		public ExcluirApicredentialsCommandValidation()
		{
			    ValidateId();
		}
	}
}
