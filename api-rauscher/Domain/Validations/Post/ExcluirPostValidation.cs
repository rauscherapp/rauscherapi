using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirPostCommandValidation : PostCommandValidation<ExcluirPostCommand>
	{
		public ExcluirPostCommandValidation()
		{
			    ValidateId();
		}
	}
}
