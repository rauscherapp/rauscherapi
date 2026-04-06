using System;

namespace APIs.Security.JWT;

public sealed class DuplicateUserException : Exception
{
    public DuplicateUserException()
        : base("J\u00E1 existe um usu\u00E1rio cadastrado com este e-mail.")
    {
    }
}
