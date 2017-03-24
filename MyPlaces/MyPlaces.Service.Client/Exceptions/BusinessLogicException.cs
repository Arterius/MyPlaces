using System;

namespace MyPlaces.Service.Client.Exceptions
{
    public class BusinessLogicException : BaseException
    {
        public BusinessLogicException() : base() { }

        public BusinessLogicException(string message) : base(message) { }

        public BusinessLogicException(string message, Exception inner) : base(message, inner) { }
    }
}
