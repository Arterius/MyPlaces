using System;

namespace MyPlaces.Service.Client.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException() : base() { }

        public BaseException(string message) : base(message) { }

        public BaseException(string message, Exception inner) : base(message, inner) { }
    }
}
