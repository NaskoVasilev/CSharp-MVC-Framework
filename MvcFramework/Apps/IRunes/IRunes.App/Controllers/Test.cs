using System.Xml.Serialization;

namespace IRunes.App.Controllers
{
	[XmlType]
	public class Test
	{
		public Test()
		{
		}

		public Test(string name, int age, bool isValid)
		{
			Name = name;
			Age = age;
			IsValid = isValid;
		}

		[XmlElement]
		public string Name { get; set; }

		[XmlElement]
		public int Age { get; set; }

		[XmlElement]
		public bool IsValid { get; set; }
	}
}
