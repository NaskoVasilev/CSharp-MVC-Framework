using System;
using System.Text.RegularExpressions;

namespace MvcFramework.Attributes.Validation
{
	public class RegexAttribute : ValidationAttribute
	{
		private readonly string pattern;

		public RegexAttribute(string pattern, string propertyName = null, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			this.pattern = pattern;
			if(propertyName != null)
			{
				ErrorMessage = $"{propertyName} is invalid!";
			}
		}

		public override bool IsValid(object value)
		{
			string valueAsString = (string)Convert.ChangeType(value, typeof(string));
			return Regex.IsMatch(valueAsString, pattern);
		}
	}
}
