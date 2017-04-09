using System;
using System.Runtime.Serialization;

namespace HackathonWork
{
    [Serializable]
    internal class InvalidInputException : Exception
    {
        private string action;
        private string v;

        public InvalidInputException()
        {
        }

        public InvalidInputException(string message) : base(message)
        {
        }

        public InvalidInputException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidInputException(string v, string action)
        {
            this.v = v;
            this.action = action;
        }

        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}