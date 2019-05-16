using MvcFramework.HTTP.Common;

namespace MvcFramework.HTTP.Headers
{
	public class HttpHeader
	{
		public HttpHeader(string key, string value)
		{
			CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
			CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

			this.Key = key;
			this.Value = value;
		}

		public string Key { get; set; }

		public string Value { get; set; }

		public override string ToString() => $"{Key}: {Value}";
	}
}
