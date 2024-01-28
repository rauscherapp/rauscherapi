using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class ExcluirFolderCommandValidation : FolderCommandValidation<ExcluirFolderCommand>
	{
		public ExcluirFolderCommandValidation()
		{
			    ValidateCdFolder();
		}
	}
}
