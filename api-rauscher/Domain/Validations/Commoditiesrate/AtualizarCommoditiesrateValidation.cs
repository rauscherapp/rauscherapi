using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class AtualizarCommoditiesRateCommandValidation : CommoditiesRateCommandValidation<AtualizarCommoditiesRateCommand>
	{
		public AtualizarCommoditiesRateCommandValidation()
		{
			    ValidateId();
		}
	}
}
