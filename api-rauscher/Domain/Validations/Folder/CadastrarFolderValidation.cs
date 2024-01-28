using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
	    public class CadastrarFolderCommandValidation : FolderCommandValidation<CadastrarFolderCommand>
	{
		public CadastrarFolderCommandValidation()
		{
			    ValidateCdFolder();
		}
	}
}
