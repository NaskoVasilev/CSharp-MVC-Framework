using MvcFramework.WebServer.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MvcFramework.WebServer
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
			this.listener = new TcpListener(IPAddress.Parse(LocalhostIpAdderss), port);
		}

		public void Run()
		{
			this.listener.Start();
			this.isRunning = true;
			Console.WriteLine($"Server start at http://{LocalhostIpAdderss}:{this.port}");

			while (isRunning)
			{
				Console.WriteLine("Waiting for client...");
				Socket client = this.listener.AcceptSocket();
				this.Listen(client);
			}
		}

		private void Listen(Socket client)
		{
			ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRoutingTable);
			connectionHandler.ProccessRequest();
		}
	}
}
