namespace MvcFramework
{
	public static class WebHost
	{
		public static void Start(IMvcApplication application)
		{
			application.Configure();
		}
	}
}
