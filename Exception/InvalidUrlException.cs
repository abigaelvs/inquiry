using System;

namespace InqService.Exceptions
{
    public class InvalidUrlException : SystemException
    {
        public InvalidUrlException() : base() { }
        public InvalidUrlException(string message) : base(message) { }
        public InvalidUrlException(string message, Exception ex) : base(message, ex) { }
    }
}
