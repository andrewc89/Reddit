using System;
using System.Runtime.Serialization;

namespace Reddit.Exceptions
{
    internal class NotEnoughKarmaException : Exception
    {
        public NotEnoughKarmaException()
        {
        }

        public NotEnoughKarmaException(string Message)
            : base(Message)
        {
        }

        public NotEnoughKarmaException(string Message, Exception InnerException)
            : base(Message, InnerException)
        {
        }

        protected NotEnoughKarmaException(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
        }
    }
}