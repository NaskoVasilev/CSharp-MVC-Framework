using System;
using System.Collections.Generic;
using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.WebServer.Routing.Contracts;

namespace MvcFramework.WebServer.Routing
{
	public class ServerRoutingTable : IServerRoutingTable
	{
		private readonly Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>> routes;

		public ServerRoutingTable()
		{
			this.routes = new Dictionary<HttpRequestMethod, Dictionary<string, Func<IHttpRequest, IHttpResponse>>>()
			{
				[HttpRequestMethod.Delete] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
				[HttpRequestMethod.Get] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
				[HttpRequestMethod.Post] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>(),
				[HttpRequestMethod.Put] = new Dictionary<string, Func<IHttpRequest, IHttpResponse>>()

			};
		}

		public void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func)
		{
			CoreValidator.ThrowIfNull(method, nameof(method));
			CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));
			CoreValidator.ThrowIfNull(func, nameof(func));

			this.routes[method][path] = func;
		}

		public bool Contains(HttpRequestMethod method, string path)
		{
			CoreValidator.ThrowIfNull(method, nameof(method));
			CoreValidator.ThrowIfNullOrEmpty(path, nameof(path));

			bool contains = this.routes.ContainsKey(method) && this.routes[method].ContainsKey(path);
			return contains;
		}

		public Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod method, string path)
		{
			if(this.Contains(method, path))
			{
				return this.routes[method][path];
			}

			return null;
		}
	}
}
