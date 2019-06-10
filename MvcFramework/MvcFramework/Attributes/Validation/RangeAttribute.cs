using System;

namespace MvcFramework.Attributes.Validation
{
	public class RangeAttribute : ValidationAttribute
	{
		private readonly object minValue;
		private readonly object maxValue;
		private readonly Type objectType;

		public RangeAttribute(int minValue, int maxValue, string propertyName, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.objectType = typeof(int);
			SetErrorMessage(propertyName);
		}

		public RangeAttribute(double minValue, double maxValue, string propertyName = null, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.objectType = typeof(double);
			SetErrorMessage(propertyName);
		}

		public RangeAttribute(Type type, string minValue, string maxValue, string propertyName = null, string errorMessage = DefaultErrorMessage) : base(errorMessage)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.objectType = type;
			SetErrorMessage(propertyName);
		}

		public override bool IsValid(object value)
		{
			if (objectType == typeof(int))
			{
				return (int)value >= (int)minValue && (int)value <= (int)maxValue;
			}
			else if (objectType == typeof(double))
			{
				return (double)value >= (double)minValue && (double)value <= (double)maxValue;
			}
			else if (objectType == typeof(decimal))
			{
				decimal minValueAsDecimal = decimal.Parse((string)minValue);
				decimal maxValueAsDecimal = decimal.Parse((string)maxValue);

				return (decimal)value >= minValueAsDecimal && (decimal)value <= maxValueAsDecimal;
			}

			return false;
		}

		private void SetErrorMessage(string propertyName)
		{
			if (propertyName != null)
			{
				ErrorMessage = $"The {propertyName} must be between {minValue} and {maxValue}";
			}
		}
	}
}
