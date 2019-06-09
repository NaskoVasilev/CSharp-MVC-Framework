using System;

namespace MvcFramework.SandBox
{
	public class StringLengthAttribute : ValidationAttribute
	{
		private readonly int minLength;
		private readonly int maxLength;

		public StringLengthAttribute(int minLength, int maxLength, string errorMessage) : base(errorMessage)
		{
			this.minLength = minLength;
			this.maxLength = maxLength;
		}

		public override bool IsValid(object value)
		{
			string valueAsString = (string)Convert.ChangeType(value, typeof(string));

			if(valueAsString.Length >= minLength && valueAsString.Length <= maxLength)
			{
				return true;
			}
			return false;
		}
	}
}
