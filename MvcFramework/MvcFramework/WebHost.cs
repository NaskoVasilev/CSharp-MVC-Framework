﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcFramework.HTTP.Enums;
using MvcFramework.Routing;
using MvcFramework.Routing.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.Attributes.Http;
using MvcFramework.Attributes.Action;

namespace MvcFramework
{
	public static class WebHost
	{
		public static void Start(IMvcApplication application)
		{
			IServerRoutingTable serverRoutingTable = new ServerRoutingTable();
			AutoRegisterRoutes(application, serverRoutingTable);

			application.Configure(serverRoutingTable);

			Server server = new Server(8000, serverRoutingTable);
			server.Run();
		}

		private static void AutoRegisterRoutes(IMvcApplication application, IServerRoutingTable serverRoutingTable)
		{
			IEnumerable<Type> controllers = application.GetType().Assembly
				.GetTypes()
				.Where(type => type.IsClass && !type.IsAbstract && typeof(Controller).IsAssignableFrom(type));

			foreach (var controller in controllers)
			{
				IEnumerable<MethodInfo> actions = controller.GetMethods(BindingFlags.DeclaredOnly |
					BindingFlags.Instance | BindingFlags.Public)
					.Where(m => !m.IsSpecialName && !m.IsVirtual && m.GetCustomAttribute<NonActionAttribute>() == null);

				string controllerName = controller.Name.Replace("Controller", "");

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
						object controllerInstance = Activator.CreateInstance(controller);
						object response = action.Invoke(controllerInstance, new[] { request });
						return response as IHttpResponse;
					});
				}
			}

		}
	}
}
