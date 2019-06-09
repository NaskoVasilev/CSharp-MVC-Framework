namespace MvcFramework.SandBox
{
	public class User
	{
		[StringLength(3, 20, "The username must be between 3 and 20 characters.")]
		public string Username { get; set; }
	}
}
