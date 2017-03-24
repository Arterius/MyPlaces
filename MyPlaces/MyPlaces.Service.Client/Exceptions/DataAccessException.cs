using System;

namespace MyPlaces.Service.Client.Exceptions
{
    public class DataAccessException : BaseException
    {
        public DataAccessException() : base() { }

        public DataAccessException(string message) : base(message) { }

        public DataAccessException(string message, Exception inner) : base(message, inner) { }
    }
}
