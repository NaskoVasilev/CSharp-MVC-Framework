namespace MvcFramework.Attributes.Validation
{
	public class RequiredAttribute : ValidationAttribute
	{
		public RequiredAttribute(string errorMessage) : base(errorMessage)
		{
		}

		public override bool IsValid(object value)
		{
			return value != null;
		}
	}
}
