using System;

namespace MvcFramework.Attributes.Validation
{
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class ValidationAttribute : Attribute
	{
		public ValidationAttribute(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public string ErrorMessage { get; }

		public abstract bool IsValid(object value);
	}
}
