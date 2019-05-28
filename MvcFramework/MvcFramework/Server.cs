using MvcFramework.Routing.Contracts;
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

		private bool isRunning;

		public Server(int port, IServerRoutingTable serverRoutingTable)
		{
			this.port = port;
			this.serverRoutingTable = serverRoutingTable;
			listener = new TcpListener(IPAddress.Parse(LocalhostIpAdderss), port);
		}

		public void Run()
		{
			listener.Start();
			isRunning = true;
			Console.WriteLine($"Server start at http://{LocalhostIpAdderss}:{port}");

			while (isRunning)
			{
				Console.WriteLine("Waiting for client...");
				Socket client = listener.AcceptSocketAsync().GetAwaiter().GetResult();
				Task.Run(() => Listen(client));
			}
		}

		private async Task Listen(Socket client)
		{
			ConnectionHandler connectionHandler = new ConnectionHandler(client, serverRoutingTable);
			await connectionHandler.ProccessRequestAsync();
		}
	}
}
