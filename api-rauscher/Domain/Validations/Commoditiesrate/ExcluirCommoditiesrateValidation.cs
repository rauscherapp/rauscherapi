using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirCommoditiesRateCommandValidation : CommoditiesRateCommandValidation<ExcluirCommoditiesRateCommand>
	{
		public ExcluirCommoditiesRateCommandValidation()
		{
			    ValidateId();
		}
	}
}
