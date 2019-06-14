using MvcFramework;

namespace Musaca.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebHost.Start(new Startup());
		}
	}
}
