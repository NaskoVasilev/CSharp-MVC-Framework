using MvcFramework.HTTP.Common;
using MvcFramework.HTTP.Enums;
using MvcFramework.HTTP.Exceptions;
using MvcFramework.HTTP.Headers;
using MvcFramework.HTTP.Headers.Contracts;
using MvcFramework.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MvcFramework.HTTP.Requests
{
	public class HttpRequest : IHttpRequest
	{
		public HttpRequest(string requestString)
		{
			CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

			this.FormData = new Dictionary<string, object>();
			this.QueryData = new Dictionary<string, object>();
			this.Headers = new HttpHeaderCollection();

			this.ParseRequest(requestString);
		}

		public string Path { get; private set; }

		public string Url { get; private set; }

		public Dictionary<string, object> FormData { get; }

		public Dictionary<string, object> QueryData { get; }

		public IHttpHeaderCollection Headers { get; }

		public HttpRequestMethod RequestMethod { get; private set; }

		private bool IsValidRequestLine(string[] requsetLineParams)
		{
			if (requsetLineParams.Length != 3 || requsetLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
			{
				return false;
			}

			if (!Enum.TryParse(requsetLineParams[0], true, out HttpRequestMethod method))
			{
				return false;
			}

			return true;
		}

		private bool IsValidRequestQueryString(string queryString)
		{
			Regex regex = new Regex(GlobalConstants.QueryStringPattern);
			return regex.IsMatch(queryString);
		}

		private void ParseRequestMethod(string[] requestLineParams)
		{
			Enum.TryParse(requestLineParams[0], true, out HttpRequestMethod method);
			this.RequestMethod = method;
		}

		private void ParseRequestUrl(string[] requestLineParams)
		{
			this.Url = requestLineParams[1];
		}

		private void ParseRequestPath()
		{
			this.Path = this.Url.Split('?')[0];
		}

		private void ParseRequestHeaders(string[] requestHeaderLines)
		{
			foreach (var headerKeyValuePair in requestHeaderLines)
			{
				if (string.IsNullOrEmpty(headerKeyValuePair))
				{
					break;
				}

				string[] keyValuePairArray = headerKeyValuePair.Split(new[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				HttpHeader header = new HttpHeader(keyValuePairArray[0], keyValuePairArray[1]);
				this.Headers.AddHeader(header);
			}
		}

		private void ParseRequestCokies()
		{
			throw new NotImplementedException();
		}

		private void ParseQueryParameters()
		{
			string[] urlElements = this.Url.Split('?');
			if(urlElements.Length < 2)
			{
				return;
			}

			string queryString = urlElements[1];

			if (string.IsNullOrEmpty(queryString) || !IsValidRequestQueryString(queryString))
			{
				throw new BadRequestException(GlobalConstants.QueryStringExceptionMessage);
			}

			string[] parametersArray = queryString.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var parameter in parametersArray)
			{
				string[] keyValuePair = parameter.Split('=');
				//TODO fix if have parameters with equal names
				this.QueryData.Add(keyValuePair[0], keyValuePair[1]);
			}
		}
		private void ParseFormDataParameters(string formData)
		{
			string[] parametersArray = formData.Split(new[] { '&' }, StringSplitOptions.None);

			foreach (var parameter in parametersArray)
			{
				string[] keyValuePair = parameter.Split('=');
				//TODO fix if have parametesr with equal names
				this.FormData.Add(keyValuePair[0], keyValuePair[1]);
			}
		}
		private void ParseRequestParameters(string formData)
		{
			this.ParseFormDataParameters(formData);
			this.ParseQueryParameters();
		}

		private void ParseRequest(string requestString)
		{
			string[] splitRequestContent = requestString.Split(new[] { GlobalConstants.HttpNewLine }, StringSplitOptions.None);

			string[] requestLineParams = splitRequestContent[0].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

			if (!this.IsValidRequestLine(requestLineParams))
			{
				throw new BadRequestException();
			}

			this.ParseRequestMethod(requestLineParams);
			this.ParseRequestUrl(requestLineParams);
			this.ParseRequestPath();

			this.ParseRequestHeaders(splitRequestContent.Skip(1).ToArray());
			this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
		}
	}
}
