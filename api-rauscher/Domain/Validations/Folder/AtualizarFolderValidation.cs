using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class AtualizarFolderCommandValidation : FolderCommandValidation<AtualizarFolderCommand>
	{
		public AtualizarFolderCommandValidation()
		{
			    ValidateCdFolder();
		}
	}
}
