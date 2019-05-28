namespace IRunes.Models
{
	public class User : BaseModel
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }
	}
}
