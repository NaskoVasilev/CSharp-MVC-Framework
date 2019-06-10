using System;

namespace MvcFramework.Attributes.Validation
{
	public class StringLengthAttribute : ValidationAttribute
	{
		private readonly int minLength;
		private readonly int maxLength;

		public StringLengthAttribute(int minLength, int maxLength, string propertyName = null, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;

			if(propertyName != null)
			{
				ErrorMessage = $"The {propertyName}'s length must be between {minLength} and {maxLength}!";
			}
		}

		public override bool IsValid(object value)
		{
			if(value == null)
			{
				return false;
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
