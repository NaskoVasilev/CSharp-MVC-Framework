using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcFramework.SandBox
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> errors = new List<string>();

			User user = new User { Username = "Valid" };

			var objectProperties = user.GetType().GetProperties();

			foreach (var objectProperty in objectProperties)
			{
				var validationAttributes = objectProperty
					.GetCustomAttributes()
					.Where(a => a is ValidationAttribute)
					.Cast<ValidationAttribute>()
					.ToList();

				foreach (var validationAttribute in validationAttributes)
				{
					var propertyValue = objectProperty.GetValue(user);
					bool isValid = validationAttribute.IsValid(propertyValue);
					if (!isValid)
					{
						errors.Add(validationAttribute.ErrorMessage);
					}
				}
			}

			Console.WriteLine(string.Join(Environment.NewLine, errors));
		}
	}
}
