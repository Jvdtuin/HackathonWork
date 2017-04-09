using System;
using System.Runtime.Serialization;

namespace HackathonWork
{
    [Serializable]
    internal class LostException : Exception
    {
        private int source;
        private string v;

        public LostException()
        {
        }

        public LostException(string message) : base(message)
        {
        }

        public LostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public LostException(string v, int source)
        {
            this.v = v;
            this.source = source;
        }

        protected LostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}