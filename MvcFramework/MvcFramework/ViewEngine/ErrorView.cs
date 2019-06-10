using MvcFramework.Identity;
using MvcFramework.Validation;

namespace MvcFramework.ViewEngine
{
	public class ErrorView : IView
	{
		private readonly string errors;

		public ErrorView(string errors)
		{
			this.errors = errors;
		}

		public string GetHtml(object model, ModelStateDictionary modelState, Principal user)
		{
			return errors;
		}
	}
}
