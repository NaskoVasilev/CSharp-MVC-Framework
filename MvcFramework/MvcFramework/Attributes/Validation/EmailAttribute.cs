using System;
using System.Text.RegularExpressions;

namespace MvcFramework.Attributes.Validation
{
	public class EmailAttribute : ValidationAttribute
	{
		private const string DefailtEmailErrorMessage = "The email is not valid!";

		public EmailAttribute(string errorMessage = DefailtEmailErrorMessage) : base(errorMessage)
		{
		}

		public override bool IsValid(object value, string propertyName)
		{
			string valueAsString = (string)Convert.ChangeType(value, typeof(string));
			return Regex.IsMatch(valueAsString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		}
	}
}
