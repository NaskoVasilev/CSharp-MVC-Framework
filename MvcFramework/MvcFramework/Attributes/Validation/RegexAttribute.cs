using System;
using System.Text.RegularExpressions;

namespace MvcFramework.Attributes.Validation
{
	public class RegexAttribute : ValidationAttribute
	{
		private readonly string pattern;

		public RegexAttribute(string pattern, string errorMessage) : base(errorMessage)
		{
			this.pattern = pattern;
		}

		public override bool IsValid(object value)
		{
			string valueAsString = (string)Convert.ChangeType(value, typeof(string));
			return Regex.IsMatch(valueAsString, pattern);
		}
	}
}
