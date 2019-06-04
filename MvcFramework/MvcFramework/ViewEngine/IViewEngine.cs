using MvcFramework.Identity;

namespace MvcFramework.ViewEngine
{
	public interface IViewEngine
	{
		string GetHtml<T>(string viewContent, T model, Principal user = null);
	}
}
