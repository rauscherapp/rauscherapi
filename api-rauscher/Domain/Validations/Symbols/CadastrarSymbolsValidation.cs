using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class CadastrarSymbolsCommandValidation : SymbolsCommandValidation<CadastrarSymbolsCommand>
	{
		public CadastrarSymbolsCommandValidation()
		{
			    ValidateId();
		}
	}
}
