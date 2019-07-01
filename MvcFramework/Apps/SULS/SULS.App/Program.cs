using MvcFramework;

namespace SULS.App
{
	public class Program
    {
        public static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}