using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class CadastrarCommoditiesRateCommandValidation : CommoditiesRateCommandValidation<CadastrarCommoditiesRateCommand>
	{
		public CadastrarCommoditiesRateCommandValidation()
		{
			    ValidateId();
		}
	}
}
