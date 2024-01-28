using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class FolderCommandValidation<T> : AbstractValidator<T> where T : FolderCommand
	{
		protected void ValidateCdFolder()
		{
			RuleFor(c => c.ID)
			.NotEmpty().WithMessage("Codigo Folder Vazio");
		}
	}
}
