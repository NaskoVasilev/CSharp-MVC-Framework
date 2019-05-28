using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Sessions;
using MvcFramework.HTTP.Sessions.Contracts;
using System.Collections.Concurrent;

namespace MvcFramework.Sessions
{
	public class HttpSessionStorage
	{
		public const string SessionCookieKey = "SIS_ID";

		private static readonly ConcurrentDictionary<string, IHttpSession> sessions =
			new ConcurrentDictionary<string, IHttpSession>();

		public static IHttpSession GetSession(string id)
		{
			CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

			return sessions.GetOrAdd(id, new HttpSession(id));
		}
	}
}
