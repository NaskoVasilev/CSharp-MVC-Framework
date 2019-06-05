using System;
using System.Threading;

namespace MvcFramework.Logging
{
	public class ConsoleLogger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{Thread.CurrentThread.ManagedThreadId}] {message}");
		}
	}
}
