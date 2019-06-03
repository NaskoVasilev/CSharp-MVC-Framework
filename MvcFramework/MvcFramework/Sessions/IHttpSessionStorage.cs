using MvcFramework.HTTP.Sessions.Contracts;

namespace MvcFramework.Sessions
{
	public interface IHttpSessionStorage
	{
		IHttpSession GetSession(string id);

		bool ContainsSession(string id);
	}
}
