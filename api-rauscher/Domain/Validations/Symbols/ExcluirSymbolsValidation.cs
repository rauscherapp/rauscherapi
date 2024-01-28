using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirSymbolsCommandValidation : SymbolsCommandValidation<ExcluirSymbolsCommand>
	{
		public ExcluirSymbolsCommandValidation()
		{
			    ValidateId();
		}
	}
}
