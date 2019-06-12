using System;

namespace MvcFramework.Attributes.Validation
{
	public class StringLengthAttribute : ValidationAttribute
	{
		private readonly int minLength;
		private readonly int maxLength;

		public StringLengthAttribute(int minLength, int maxLength, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		public override bool IsValid(object value, string propertyName)
		{
			if (value == null)
			{
				return false;
			}

			if (ErrorMessage == DefaultErrorMessage)
			{
				ErrorMessage = $"The {propertyName}'s length must be between {minLength} and {maxLength}!";
			}

			string valueAsString = (string)Convert.ChangeType(value, typeof(string));

			if (valueAsString.Length >= minLength && valueAsString.Length <= maxLength)
			{
				return true;
			}
			return false;
		}
	}
}
