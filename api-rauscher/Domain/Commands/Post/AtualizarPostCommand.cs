using Domain.Validations;

namespace Domain.Commands
{
    public class AtualizarPostCommand : PostCommand
    {
        public override bool IsValid()
        {
            ValidationResult = new AtualizarPostCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
