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
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.AutoMapper;
using MvcFramework.Utils;
using MvcFramework.Validation;
using MvcFramework.Attributes.Validation;

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
						return ProcessRequest(serviceProvider, request, controllerType, controllerAuthorizeAttribute, action);
					});
				}
			}
		}

		private static IActionResult ProcessRequest(IServiceProvider serviceProvider, IHttpRequest request,
			System.Type controllerType, AuthorizeAttribute controllerAuthorizeAttribute, MethodInfo action)
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

			List<object> actionParameters = new List<object>();

			foreach (var parameter in action.GetParameters())
			{
				object parameterValue = GetParamterValueFromHttpData(request, parameter.ParameterType, parameter.Name);


				ModelStateDictionary modelState = ValidateObject(parameterValue);
				typeof(Controller)
					.GetProperty("ModelState", BindingFlags.Instance | BindingFlags.NonPublic)
					.SetValue(controllerInstance, modelState);
				
				actionParameters.Add(parameterValue);
			}

			object response = action.Invoke(controllerInstance, actionParameters.ToArray());
			return response as IActionResult;
		}

		private static ModelStateDictionary ValidateObject(object parameterValue)
		{
			ModelStateDictionary modelState = new ModelStateDictionary();
			var objectProperties = parameterValue.GetType().GetProperties();

			foreach (var objectProperty in objectProperties)
			{
				var validationAttributes = objectProperty
					.GetCustomAttributes()
					.Where(a => a is ValidationAttribute)
					.Cast<ValidationAttribute>()
					.ToList();

				foreach (var validationAttribute in validationAttributes)
				{
					var propertyValue = objectProperty.GetValue(parameterValue);
					bool isValid = validationAttribute.IsValid(propertyValue);
					if (!isValid)
					{
						modelState.Add(objectProperty.Name, validationAttribute.ErrorMessage);
					}
				}
			}

			return modelState;
		}

		private static object GetParamterValueFromHttpData(IHttpRequest request, System.Type parameterType, string parameterName)
		{
			ISet<string> httpDataValue = TryGetHttpParameter(request, parameterName);

			object parameterValue = null;
			try
			{
				string httpStringValue = httpDataValue.FirstOrDefault();
				parameterValue = System.Convert.ChangeType(httpStringValue, parameterType);
			}
			catch (System.Exception)
			{
				if (ReflectionUtils.IsGenericCollection(parameterType))
				{
					System.Type collecionElementsType = parameterType.GetGenericArguments()[0];
					var collection = httpDataValue.Select(t => System.Convert.ChangeType(t, collecionElementsType));
					parameterValue = CastUtils.CastCollectionToList(collection, collecionElementsType);
				}
				else if (parameterType.IsArray)
				{
					System.Type arrayElementsType = parameterType.GetElementType();
					var array = httpDataValue.Select(t => System.Convert.ChangeType(t, arrayElementsType));
					parameterValue = CastUtils.CastCollectionToArray(array, arrayElementsType);
				}
				else
				{
					parameterValue = System.Activator.CreateInstance(parameterType);
					foreach (var property in parameterType.GetProperties())
					{
						object propertyValue = GetParamterValueFromHttpData(request, property.PropertyType, property.Name);
						property.SetMethod.Invoke(parameterValue, new object[] { propertyValue });
					}
				}
			}

			return parameterValue;
		}

		private static ISet<string> TryGetHttpParameter(IHttpRequest request, string parameterName)
		{
			ISet<string> httpDataValue = null;
			parameterName = parameterName.ToLower();

			if (request.QueryData.ContainsKey(parameterName))
			{
				httpDataValue = request.QueryData[parameterName];
			}
			else if (request.FormData.ContainsKey(parameterName))
			{
				httpDataValue = request.FormData[parameterName];
			}

			return httpDataValue;
		}
	}
}
