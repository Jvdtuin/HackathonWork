using System;
using System.Runtime.Serialization;

namespace HackathonWork
{
	[Serializable]
	internal class GameOverException : Exception
	{
		public GameOverException()
		{
		}

		public GameOverException(string message) : base(message)
		{
		}

		public GameOverException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected GameOverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}