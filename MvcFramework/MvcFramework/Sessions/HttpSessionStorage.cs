using MvcFramework.Common;
using MvcFramework.HTTP.Sessions;
using MvcFramework.HTTP.Sessions.Contracts;
using System.Collections.Concurrent;

namespace MvcFramework.Sessions
{
	public class HttpSessionStorage : IHttpSessionStorage
	{
		public const string SessionCookieKey = "SIS_ID";

		private readonly ConcurrentDictionary<string, IHttpSession> sessions;

		public HttpSessionStorage()
		{
			this.sessions = new ConcurrentDictionary<string, IHttpSession>();
		}

		public IHttpSession GetSession(string id)
		{
			CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

			return sessions.GetOrAdd(id, new HttpSession(id));
		}

		public bool ContainsSession(string sessionId)
		{
			CoreValidator.ThrowIfNullOrEmpty(sessionId, nameof(sessionId));

			return sessions.ContainsKey(sessionId);
		}
	}
}
