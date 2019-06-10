namespace MvcFramework.Attributes.Validation
{
	public class RequiredAttribute : ValidationAttribute
	{
		public RequiredAttribute(string propertyName = null, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			if(propertyName != null)
			{
				ErrorMessage = $"The {propertyName} is required";
			}
		}

		public override bool IsValid(object value)
		{
			return value != null;
		}
	}
}
