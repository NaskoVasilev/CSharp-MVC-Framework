using MvcFramework.Validation;
using MvcFramework.ViewEngine;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MvcFramework.Tests
{
	public class TestSisViewEngine
	{
		[Theory]
		[InlineData("TestWithoutCSharpCode")]
		[InlineData("UseForForeachAndIf")]
		[InlineData("UseModelData")]
		[InlineData("OneLineCSharpCode")]
		public void TestGetHtml(string testFileName)
		{
			IViewEngine viewEngine = new SisViewEngine();

			string viewFileName = $"ViewTests/{testFileName}.html";
			string expectedViewFileName = $"ViewTests/{testFileName}.Result.html";

			string viewContent = File.ReadAllText(viewFileName);
			string expectedViewContent = File.ReadAllText(expectedViewFileName);

			TestViewModel model = new TestViewModel
			{
				StringValue = "str",
				ListValues = new List<string>() { "123", string.Empty, "val1" }
			};

			ModelStateDictionary modelState = new ModelStateDictionary();
			string actualViewContent = viewEngine.GetHtml<TestViewModel>(viewContent, model, modelState);
			Assert.Equal(expectedViewContent, actualViewContent);
		}

		[Theory]
		[InlineData("TestWithoutCSharpCode")]
		[InlineData("UseForForeachAndIf")]
		public void TestGetHtmlWithoutModel(string testFileName)
		{
			IViewEngine viewEngine = new SisViewEngine();

			string viewFileName = $"ViewTests/{testFileName}.html";
			string expectedViewFileName = $"ViewTests/{testFileName}.Result.html";

			string viewContent = File.ReadAllText(viewFileName);
			string expectedViewContent = File.ReadAllText(expectedViewFileName);

			string actualViewContent = viewEngine.GetHtml<object>(viewContent, null, new ModelStateDictionary());
			Assert.Equal(expectedViewContent, actualViewContent);
		}

		[Theory]
		[InlineData("InlineCSharpCode")]
		[InlineData("MultilineCSharpCode")]
		public void TestGetHtmlWithAlbumViewModel(string testFileName)
		{
			IViewEngine viewEngine = new SisViewEngine();

			string viewFileName = $"ViewTests/{testFileName}.html";
			string expectedViewFileName = $"ViewTests/{testFileName}.Result.html";

			string viewContent = File.ReadAllText(viewFileName);
			string expectedViewContent = File.ReadAllText(expectedViewFileName);

			AlbumTestViewModel model = new AlbumTestViewModel
			{
				Name = "Album Name",
				Id = 1,
				Price = 5
			};

			string actualViewContent = viewEngine.GetHtml(viewContent, model, new ModelStateDictionary());
			Assert.Equal(expectedViewContent, actualViewContent);
		}
	}
}
