using System;

namespace MvcFramework.Attributes.Validation
{
	[AttributeUsage(AttributeTargets.Property)]
	public abstract class ValidationAttribute : Attribute
	{
		protected const string DefaultErrorMessage = "Invalid data!";

		public ValidationAttribute(string errorMessage)
		{
			ErrorMessage = errorMessage;
		}

		public string ErrorMessage { get; protected set; }

		public abstract bool IsValid(object value, string propertyName);
	}
}
