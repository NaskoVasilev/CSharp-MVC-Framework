using System;

namespace MvcFramework.HTTP.Exceptions
{
	public class BadRequestException : Exception
	{
		private const string DefaultMessage = "The Request was malformed or contains unsupported elements.";

		public BadRequestException() : this(DefaultMessage)
		{
		}

		public BadRequestException(string message) : base(message)
		{
		}
	}
}
