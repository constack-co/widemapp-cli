using System;

namespace Constack.Widemapp.Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("You don't have permission for this")
        {
        }
    }
}
