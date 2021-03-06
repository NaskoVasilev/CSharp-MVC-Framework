﻿using MvcFramework.Common;
using MvcFramework.HTTP.Cookies;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Exceptions;
using MvcFramework.HTTP.Requests;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.Results;
using MvcFramework.Routing.Contracts;
using MvcFramework.Sessions;
using System;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvcFramework
{
	public class ConnectionHandler
	{
		private readonly Socket client;

		private readonly IServerRoutingTable serverRoutingTable;

		private readonly IHttpSessionStorage httpSessionStorage;

		public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable, IHttpSessionStorage httpSessionStorage)
		{
			CoreValidator.ThrowIfNull(client, nameof(client));
			CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));
			CoreValidator.ThrowIfNull(httpSessionStorage, nameof(httpSessionStorage));

			this.client = client;
			this.serverRoutingTable = serverRoutingTable;
			this.httpSessionStorage = httpSessionStorage;
		}

		public async Task ProccessRequestAsync()
		{
			IHttpResponse httpResponse = null;

			try
			{
				IHttpRequest httpRequest = await ReadRequestAsync();

				if (httpRequest != null)
				{
					Console.WriteLine($"Proccessing {httpRequest.RequestMethod} {httpRequest.Path} ...");
					string sessionId = SetRequestSession(httpRequest);
					httpResponse = HandleRequest(httpRequest);
					SetResponseSession(httpResponse, sessionId);
				}
			}
			catch (BadRequestException e)
			{
				httpResponse = new TextResult(e.ToString(), HttpResponseStatusCode.BadRequest);
			}
			catch (Exception e)
			{
				httpResponse = new TextResult(e.ToString(), HttpResponseStatusCode.InernalServerError);
			}

			await PrepareResponseAsync(httpResponse);
			client.Shutdown(SocketShutdown.Both);
		}

		private async Task<IHttpRequest> ReadRequestAsync()
		{
			StringBuilder result = new StringBuilder();
			ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

			while (true)
			{
				int numberOfBytesRead = await client.ReceiveAsync(data.Array, SocketFlags.None);

				if (numberOfBytesRead == 0)
				{
					break;
				}

				string bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
				result.Append(bytesAsString);

				if (numberOfBytesRead < 1023)
				{
					break;
				}
			}

			if (result == null)
			{
				return null;
			}

			IHttpRequest httpRequest = new HttpRequest(result.ToString());
			return httpRequest;
		}

		private IHttpResponse HandleRequest(IHttpRequest httpRequest)
		{
			if (!serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
			{
				return ReturnIfResource(httpRequest);
			}

			IHttpResponse httpResponse = serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path)
				.Invoke(httpRequest);
			return httpResponse;
		}

		private IHttpResponse ReturnIfResource(IHttpRequest httpRequest)
		{
			string assemblyPath = Assembly.GetExecutingAssembly().Location;
			string folderPrefix = "/../../../../";
			string filePath = assemblyPath + folderPrefix + GlobalConstants.StaticFolderName + httpRequest.Path;

			if (File.Exists(filePath))
			{
				byte[] content = File.ReadAllBytes(filePath);
				return new InlineResourceResult(content, HttpResponseStatusCode.Found);
			}

			string notFoundMessage = string.Format(GlobalConstants.RouteNotFound, httpRequest.RequestMethod, httpRequest.Path);
			return new TextResult(notFoundMessage, HttpResponseStatusCode.NotFound);
		}

		private async Task PrepareResponseAsync(IHttpResponse httpResponse)
		{
			byte[] byteSegments = httpResponse.GetBytes();
			await client.SendAsync(byteSegments, SocketFlags.None);
		}

		private string SetRequestSession(IHttpRequest httpRequest)
		{
			string sessionId = null;

			if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
			{
				sessionId = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey).Value;
			}
			else
			{
				sessionId = Guid.NewGuid().ToString();
			}

			httpRequest.Session = this.httpSessionStorage.GetSession(sessionId);
			return sessionId;
		}

		private void SetResponseSession(IHttpResponse response, string sessionId)
		{
			if (sessionId != null)
			{
				response.AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId));
			}
		}
	}
}
