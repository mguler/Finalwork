using System;

namespace TR.Edu.Ankara.EUB201.Finalwork.Common.ExceptionManagement
{
    public class CustomApplicationException : Exception
    {
        public CustomApplicationException(string message) : base(message)
        {

        }
    }
}
