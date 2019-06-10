using MvcFramework.Identity;
using MvcFramework.Validation;

namespace MvcFramework.ViewEngine
{
	public interface IView
	{
		string GetHtml(object model, ModelStateDictionary modelState, Principal user);
	}
}
