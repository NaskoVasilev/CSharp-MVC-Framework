using MvcFramework.Common;
using MvcFramework.Routing.Contracts;
using MvcFramework.Sessions;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MvcFramework
{
	public class Server
	{
		private const string LocalhostIpAdderss = "127.0.0.1";

		private readonly int port;

		private readonly TcpListener listener;

		private readonly IServerRoutingTable serverRoutingTable;

		private readonly IHttpSessionStorage httpSessionStorage;

		private bool isRunning;

		public Server(int port, IServerRoutingTable serverRoutingTable, IHttpSessionStorage httpSessionStorage)
		{
			CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));
			CoreValidator.ThrowIfNull(httpSessionStorage, nameof(httpSessionStorage));

			this.port = port;
			this.serverRoutingTable = serverRoutingTable;
			listener = new TcpListener(IPAddress.Parse(LocalhostIpAdderss), port);
			this.httpSessionStorage = httpSessionStorage;
		}

		public void Run()
		{
			listener.Start();
			isRunning = true;
			Console.WriteLine($"Server start at http://{LocalhostIpAdderss}:{port}");

			while (isRunning)
			{
				Socket client = listener.AcceptSocketAsync().GetAwaiter().GetResult();
				Task.Run(() => Listen(client));
			}
		}

		private async Task Listen(Socket client)
		{
			ConnectionHandler connectionHandler = new ConnectionHandler(client, serverRoutingTable, this.httpSessionStorage);
			await connectionHandler.ProccessRequestAsync();
		}
	}
}
