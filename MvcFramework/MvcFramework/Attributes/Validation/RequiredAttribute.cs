namespace MvcFramework.Attributes.Validation
{
	public class RequiredAttribute : ValidationAttribute
	{
		public RequiredAttribute(string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
		}

		public override bool IsValid(object value, string propertyName)
		{
			if(ErrorMessage == DefaultErrorMessage)
			{
				ErrorMessage = $"The {propertyName} is required";
			}

			return value != null && !string.IsNullOrEmpty((string)value);
		}
	}
}
