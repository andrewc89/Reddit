using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    class NotLoggedInException : Exception
    {
        public NotLoggedInException()
        {
        }

        public NotLoggedInException(string Message)
            : base(Message)
        {
        }

        public NotLoggedInException(string Message, Exception InnerException)
            : base(Message, InnerException)
        {
        }

        protected NotLoggedInException(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
        }
    }
}