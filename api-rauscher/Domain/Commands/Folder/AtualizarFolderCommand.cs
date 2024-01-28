using Domain.Validations;

namespace Domain.Commands
{
	    public class AtualizarFolderCommand : FolderCommand
	{
		        public override bool IsValid()
		{
			            ValidationResult = new AtualizarFolderCommandValidation().Validate(this);
			            return ValidationResult.IsValid;
		}
	}
}
