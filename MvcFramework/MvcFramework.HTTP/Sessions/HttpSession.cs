using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Sessions.Contracts;
using System;
using System.Collections.Generic;

namespace MvcFramework.HTTP.Sessions
{
	public class HttpSession : IHttpSession
	{
		private readonly Dictionary<string, object> parameters;

		public HttpSession(string  id)
		{
			this.Id = id;
			this.parameters = new Dictionary<string, object>();
		}

		public string Id { get; }

		public void AddParameter(string name, object parameter)
		{
			parameters[name] = parameter;
		}

		public void ClearParameters()
		{
			parameters.Clear();
		}

		public bool ConstainsParameter(string name)
		{
			return parameters.ContainsKey(name);
		}

		public object GetParameter(string name)
		{
			if (!ConstainsParameter(name))
			{
				throw new ArgumentException(string.Format(GlobalConstants.ParameterDoesNotExists, name));
			}

			return parameters[name];
		}
	}
}
