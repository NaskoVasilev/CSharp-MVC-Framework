using MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes.App
{
	public static class Program
	{
		public static void Main()
		{
			WebHost.Start(new Startup());
		}
	}
}
