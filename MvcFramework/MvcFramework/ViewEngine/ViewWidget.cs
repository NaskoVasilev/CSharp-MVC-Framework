using System.IO;

namespace MvcFramework.ViewEngine
{
	public abstract class ViewWidget : IViewWidget
	{
		private const string WidgetFolderPath = "Views/Shared/Validation/";
		private const string WidgetExtension = ".html";

		public string Render()
		{
			return File.ReadAllText(WidgetFolderPath + this.GetType().Name + WidgetExtension);
		}
	}
}
