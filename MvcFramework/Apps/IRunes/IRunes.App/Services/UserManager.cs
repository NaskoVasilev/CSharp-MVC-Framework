using IRunes.Models;
using MvcFramework.HTTP.Requests.Contracts;

namespace IRunes.App.Services
{
	public class UserManager
	{
		public void SignIn(IHttpRequest request, User user)
		{
			request.Session.AddParameter("username", user.Username);
			request.Session.AddParameter("email", user.Email);
			request.Session.AddParameter("userId", user.Id);
		}


		public void SignOut(IHttpRequest request)
		{
			request.Session.ClearParameters();
		}
	}
}
