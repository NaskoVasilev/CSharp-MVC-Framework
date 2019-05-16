using System;

namespace MvcFramework.HTTP.Exceptions
{
	public class InternalServerErrorException : Exception
	{
		private const string DefaultMessage = "The Server has encountered an error.";

		public InternalServerErrorException() : this(DefaultMessage)
		{
		}

		public InternalServerErrorException(string message) : base(message)
		{
		}
	}
}
