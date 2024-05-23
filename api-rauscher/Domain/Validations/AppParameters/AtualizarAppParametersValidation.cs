using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class AtualizarAppParametersCommandValidation : AppParametersCommandValidation<AtualizarAppParametersCommand>
	{
		public AtualizarAppParametersCommandValidation()
		{
			    ValidateAppParametersId();
		}
	}
}
