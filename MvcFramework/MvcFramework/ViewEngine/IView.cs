using MvcFramework.Identity;

namespace MvcFramework.ViewEngine
{
	public interface IView
	{
		string GetHtml(object model, Principal user);
	}
}
