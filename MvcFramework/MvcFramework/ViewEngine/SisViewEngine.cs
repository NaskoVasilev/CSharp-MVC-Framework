using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MvcFramework.ViewEngine
{
	public class SisViewEngine : IViewEngine
	{
		public string GetHtml<T>(string viewContent, T model)
		{
			string cSharpCode = GetCSharpCode(viewContent);

			string code = $@"
			using System;
			using System.Linq;
			using System.Collections.Generic;
			using System.Text;
			using MvcFramework.ViewEngine;

			namespace AppViewCodeNamespace
			{{
				public class AppViewCode : IView
				{{
					public string GetHtml()
					{{
						var html = new StringBuilder();

						html.AppendLine(""Hello from our view engine"");
						List<int> numbers = Enumerable.Range(100, 5).ToList();
						foreach (var numeber in numbers)
						{{
							html.AppendLine(numeber.ToString());
						}}

						return html.ToString();
					}}
				}}
			}}";

			IView view = CompileAndInstance(code);
			var html = view.GetHtml();

			return html;
		}

		private IView CompileAndInstance(string code)
		{
			CSharpCompilation compilation = CSharpCompilation.Create("AppViewModel")
				.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
				.AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
				.AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

			var netStandardAssemblies = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();

			foreach (var assembly in netStandardAssemblies)
			{
				string assemblyLocation = Assembly.Load(assembly).Location;
				compilation = compilation.AddReferences(MetadataReference.CreateFromFile(assemblyLocation));
			}

			SyntaxTree syntaxTree = SyntaxFactory.ParseSyntaxTree(code);
			compilation = compilation.AddSyntaxTrees(syntaxTree);

			using (MemoryStream memoryStream = new MemoryStream())
			{
				EmitResult emitResult = compilation.Emit(memoryStream);

				if (!emitResult.Success)
				{
					StringBuilder errors = new StringBuilder();
					foreach (var error in emitResult.Diagnostics) //.Where(d => d.Severity == DiagnosticSeverity.Error))
					{
						errors.AppendLine(error.GetMessage());
					}

					throw new Exception("The input code cannot be compiled: \n" + errors.ToString());
				}

				memoryStream.Seek(0, SeekOrigin.Begin);
				byte[] assemblyBytes = memoryStream.ToArray();
				Assembly assembly = Assembly.Load(assemblyBytes);

				Type type = assembly.GetType("AppViewCodeNamespace.AppViewCode");
				if(type == null)
				{
					throw new NullReferenceException("AppViewCodeNamespace.AppViewCode class was not found!");
				}

				IView view = (IView)Activator.CreateInstance(type);
				if(view == null)
				{
					throw new NullReferenceException("AppViewCodeNamespace.AppViewCode class cannot be instanciated!");
				}

				return view;
			}
		}

		private string GetCSharpCode(string viewContent)
		{
			return string.Empty;
		}
	}
}
