using MvcFramework.HTTP.Enums;
using System.Text;

namespace MvcFramework.Results
{
	public class NotFoundResult : ActionResult
	{
		public NotFoundResult(string content) : base(HttpResponseStatusCode.NotFound)
		{
			this.Content = Encoding.UTF8.GetBytes(content);
		}
	}
}
