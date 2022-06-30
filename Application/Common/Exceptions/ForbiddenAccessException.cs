using System;

namespace Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base("Você não tem permissão para fazer essa ação.") {
        }
    }
}
