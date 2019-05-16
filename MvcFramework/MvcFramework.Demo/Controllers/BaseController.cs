using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.WebServer.Results;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace MvcFramework.Demo.Controllers
{
	public class BaseController
	{
		public IHttpResponse View([CallerMemberName] string view = null)
		{
			//Todo fix path
			string controllerName = this.GetType().Name.Replace("Controller", "");
			string path = $"../../../Views/{controllerName}/{view}.html";
			string viewContent = File.ReadAllText(path);

			return new HtmlResult(viewContent, HttpResponseStatusCode.Ok);
		}
	}
}
