using System;

namespace MyPlaces.Service.Client.Exceptions
{
    public class DataPaginationException : DataAccessException
    {
        public DataPaginationException() : base() { }

        public DataPaginationException(string message) : base(message) { }

        public DataPaginationException(string message, Exception inner) : base(message, inner) { }
    }
}
