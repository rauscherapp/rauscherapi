using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class AtualizarPostCommandValidation : PostCommandValidation<AtualizarPostCommand>
	{
		public AtualizarPostCommandValidation()
		{
			    ValidateId();
		}
	}
}
