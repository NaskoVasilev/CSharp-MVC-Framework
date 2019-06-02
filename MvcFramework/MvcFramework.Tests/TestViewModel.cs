using System.Collections.Generic;

namespace MvcFramework.Tests
{
	public class TestViewModel
	{
		public string StringValue { get; set; }

		public IEnumerable<string> ListValues { get; set; }
	}
}
