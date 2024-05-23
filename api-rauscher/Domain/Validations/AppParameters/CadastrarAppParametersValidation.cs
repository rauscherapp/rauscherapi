using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class CadastrarAppParametersCommandValidation : AppParametersCommandValidation<CadastrarAppParametersCommand>
	{
		public CadastrarAppParametersCommandValidation()
		{
			    ValidateAppParametersId();
		}
	}
}
