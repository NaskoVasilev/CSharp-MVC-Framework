using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MvcFramework.Extensions
{
	public static class ObjectExtensions
	{
		public static string ToXml(this object obj)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType());
			return SerializeObjectToXml(serializer, obj);
		}

		public static string ToXml(this object obj, string rootName)
		{
			XmlSerializer serializer = new XmlSerializer(obj.GetType(), new XmlRootAttribute(rootName));
			return SerializeObjectToXml(serializer, obj);
		}

		public static string ToJson(this object obj)
		{
			return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			});
		}

		private static string SerializeObjectToXml(XmlSerializer serializer, object obj)
		{
			StringBuilder sb = new StringBuilder();
			serializer.Serialize(new StringWriter(sb), obj);
			return sb.ToString().TrimEnd();
		}
	}
}
