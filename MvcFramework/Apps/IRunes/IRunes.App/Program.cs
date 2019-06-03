using IRunes.App.ViewModels;
using MvcFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
