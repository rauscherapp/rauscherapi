using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirAppParametersCommandValidation : AppParametersCommandValidation<ExcluirAppParametersCommand>
	{
		public ExcluirAppParametersCommandValidation()
		{
			    ValidateAppParametersId();
		}
	}
}
