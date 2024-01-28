using Domain.Validations;
using System;

namespace Domain.Commands
{
	    public class ExcluirPostCommand : PostCommand
	{
		public Guid id {get; set;}
		public ExcluirPostCommand(Guid id)
		{
			    ID = id;
		}
		        public override bool IsValid()
		{
			            ValidationResult = new ExcluirPostCommandValidation().Validate(this);
			            return ValidationResult.IsValid;
		}
	}
}
