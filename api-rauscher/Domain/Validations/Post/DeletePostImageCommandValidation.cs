using Domain.Commands;
using FluentValidation;

namespace Domain.Validations
{
            public class DeletePostImageCommandValidation : PostCommandValidation<DeletePostImageCommand>
        {
                public DeletePostImageCommandValidation()
                {
                            ValidateId();
                }
        }
}
