using MvcFramework.Identity;
using MvcFramework.Validation;

namespace MvcFramework.ViewEngine
{
	public interface IViewEngine
	{
		string GetHtml<T>(string viewContent, T model, ModelStateDictionary modelState, Principal user = null);
	}
}
