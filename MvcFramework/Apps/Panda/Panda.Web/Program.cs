using MvcFramework;
using System;

namespace Panda.Web
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			WebHost.Start(new Startup());
		}
	}
}
