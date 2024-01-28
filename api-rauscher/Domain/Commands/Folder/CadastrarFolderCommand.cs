using Domain.Validations;

namespace Domain.Commands
{
	    public class CadastrarFolderCommand : FolderCommand
	{
		        public override bool IsValid()
		{
			            ValidationResult = new CadastrarFolderCommandValidation().Validate(this);
			            return ValidationResult.IsValid;
		}
	}
}
