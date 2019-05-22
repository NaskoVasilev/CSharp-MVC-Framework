namespace MvcFramework.HTTP.Cookies.Contracts
{
	public interface IHttpCookieCollection
	{
		void AddCookie(HttpCookie httpCookie);

		bool ContainsCookie(string key);

		HttpCookie GetCookie(string key);

		bool HasCookies();
	}
}
