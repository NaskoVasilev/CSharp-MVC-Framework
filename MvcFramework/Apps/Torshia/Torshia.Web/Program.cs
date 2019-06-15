using MvcFramework;
using System;

namespace Torshia.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebHost.Start(new Startup());
		}
	}
}
