using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using System;

namespace MvcFramework.Routing.Contracts
{
	public interface IServerRoutingTable
	{
		void Add(HttpRequestMethod method, string path, Func<IHttpRequest, IHttpResponse> func);

		bool Contains(HttpRequestMethod method, string path);

		Func<IHttpRequest, IHttpResponse> Get(HttpRequestMethod method, string path);
	}
}
