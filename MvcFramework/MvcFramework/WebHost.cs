using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcFramework.HTTP.Enums;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Action;
using MvcFramework.Results;
using MvcFramework.Identity;
using MvcFramework.Attributes.Security;
using MvcFramework.Sessions;
using MvcFramework.Common;
using MvcFramework.DependencyContainer;
using MvcFramework.Logging;

namespace MvcFramework
{
	public static class WebHost
	{
		public static void Start(IMvcApplication application)
		{
			IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
			IHttpSessionStorage httpSessionStorage = new HttpSessionStorage();
			IServiceProvider serviceProvider = new ServiceProvider();
			serviceProvider.Add<ILogger, ConsoleLogger>();

			AutoRegisterRoutes(application, serverRoutingTable, serviceProvider);

			application.ConfigureServices(serviceProvider);
			application.Configure(serverRoutingTable);
			Server server = new Server(8000, serverRoutingTable, httpSessionStorage);
			server.Run();
		}

		private static void AutoRegisterRoutes(IMvcApplication application, 
			IServerRoutingTable serverRoutingTable, 
			IServiceProvider serviceProvider)
		{
			IEnumerable<System.Type> controllers = application.GetType().Assembly
				.GetTypes()
				.Where(type => type.IsClass && !type.IsAbstract && typeof(Controller).IsAssignableFrom(type));

			foreach (var controllerType in controllers)
			{
				IEnumerable<MethodInfo> actions = controllerType.GetMethods(BindingFlags.DeclaredOnly |
					BindingFlags.Instance | BindingFlags.Public)
					.Where(m => !m.IsSpecialName && !m.IsVirtual && m.GetCustomAttribute<NonActionAttribute>() == null);

				string controllerName = controllerType.Name.Replace("Controller", "");

				AuthorizeAttribute controllerAuthorizeAttribute = controllerType.GetCustomAttribute<AuthorizeAttribute>() as AuthorizeAttribute;

				foreach (var action in actions)
				{
					string path = $"/{controllerName}/{action.Name}";

					BaseHttpAttribute attribute = action.GetCustomAttributes()
						.LastOrDefault(a => a.GetType().IsSubclassOf(typeof(BaseHttpAttribute))) as BaseHttpAttribute;

					HttpRequestMethod requestMethod = HttpRequestMethod.Get;

					if (attribute != null)
					{
						requestMethod = attribute.HttpRequestMethod;
					}
					if (attribute?.ActionName != null)
					{
						path = $"/{controllerName}/{attribute.ActionName}";
					}
					if (attribute?.Url != null)
					{
						path = attribute.Url;
					}

					serverRoutingTable.Add(requestMethod, path, request =>
					{
						Controller controllerInstance = serviceProvider.CreateInstance(controllerType) as Controller;
						typeof(Controller).GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic)
						.SetValue(controllerInstance, request);

						Principal user = typeof(Controller).GetProperty("User", BindingFlags.Instance | BindingFlags.NonPublic)
						.GetValue(controllerInstance) as Principal;

						if (action.GetCustomAttribute<AllowAnonymousAttribute>() == null)
						{
							//Method authorize Attribute has more priority
							if (action.GetCustomAttribute<AuthorizeAttribute>() is AuthorizeAttribute authorizeAttribute &&
							!authorizeAttribute.IsAuthorized(user))
							{
								//TODO: the redirect path must be set from outside
								return new RedirectResult(GlobalConstants.RedirectPath);
							}
							else if (controllerAuthorizeAttribute != null && !controllerAuthorizeAttribute.IsAuthorized(user))
							{
								return new RedirectResult(GlobalConstants.RedirectPath);
							}
						}

						object response = action.Invoke(controllerInstance, new object[0]);
						return response as IActionResult;
					});
				}
			}
		}
	}
}
