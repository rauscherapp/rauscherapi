using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class CadastrarPostCommandValidation : PostCommandValidation<CadastrarPostCommand>
	{
		public CadastrarPostCommandValidation()
		{
			    ValidateId();
		}
	}
}
