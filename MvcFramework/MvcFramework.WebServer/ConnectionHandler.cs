using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Exceptions;
using MvcFramework.HTTP.Requests;
using MvcFramework.HTTP.Requests.Contracts;
using MvcFramework.HTTP.Responses.Contracts;
using MvcFramework.WebServer.Results;
using MvcFramework.WebServer.Routing.Contracts;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MvcFramework.WebServer
{
	public class ConnectionHandler
	{
		private readonly Socket client;

		private readonly IServerRoutingTable serverRoutingTable;

		public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
		{
			CoreValidator.ThrowIfNull(client, nameof(client));
			CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

			this.client = client;
			this.serverRoutingTable = serverRoutingTable;
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
					httpResponse = HandleRequest(httpRequest);
				}
			}
			catch (BadRequestException e)
			{
				httpResponse = new TextResult(e.Message, HttpResponseStatusCode.BadRequest);
			}
			catch(Exception e)
			{
				httpResponse = new TextResult(e.Message, HttpResponseStatusCode.InernalServerError);
			}

			await this.PrepareResponseAsync(httpResponse);
			this.client.Shutdown(SocketShutdown.Both);
		}

		private async Task<IHttpRequest> ReadRequestAsync()
		{
			StringBuilder result = new StringBuilder();
			ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

			while (true)
			{
				int numberOfBytesRead = await this.client.ReceiveAsync(data.Array, SocketFlags.None);

				if(numberOfBytesRead == 0)
				{
					break;
				}

				string bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
				result.Append(bytesAsString);

				if(numberOfBytesRead < 1023)
				{
					break;
				}
			}

			if(result == null)
			{
				return null;
			}

			IHttpRequest httpRequest = new HttpRequest(result.ToString());
			return httpRequest;
		}

		private IHttpResponse HandleRequest(IHttpRequest httpRequest)
		{
			if(!this.serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
			{
				string content = string.Format(GlobalConstants.RouteNotFound, httpRequest.RequestMethod, httpRequest.Path);
				return new TextResult(content, HttpResponseStatusCode.NotFound);
			}

			IHttpResponse httpResponse = this.serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path)
				.Invoke(httpRequest);
			return httpResponse;
		}

		private async Task PrepareResponseAsync(IHttpResponse httpResponse)
		{
			byte[] byteSegments = httpResponse.GetBytes();
			await client.SendAsync(byteSegments, SocketFlags.None);
		}
	}
}
