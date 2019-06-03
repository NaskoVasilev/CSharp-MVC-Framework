using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

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
		public string GetHtml(object model)
		{{
			var Model = {(model == null ? "new {}" : GetModelType(model))};

			var html = new StringBuilder();
			{cSharpCode}
			return html.ToString();
		}}
	}}
}}";

			IView view = CompileAndInstance(code, GetModelAssembly(model));
			var html = view.GetHtml(model).TrimEnd();
			return html;
		}

		private IView CompileAndInstance(string code, Assembly modelAssembly)
		{
			modelAssembly = modelAssembly ?? Assembly.GetEntryAssembly();

			CSharpCompilation compilation = CSharpCompilation.Create("AppViewModel")
				.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
				.AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
				.AddReferences(MetadataReference.CreateFromFile(modelAssembly.Location))
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
						Console.WriteLine(error.GetMessage());
						errors.AppendLine(error.GetMessage());
					}

					throw new Exception("The input code cannot be compiled: \n" + errors.ToString());
				}

				memoryStream.Seek(0, SeekOrigin.Begin);
				byte[] assemblyBytes = memoryStream.ToArray();
				Assembly assembly = Assembly.Load(assemblyBytes);

				Type type = assembly.GetType("AppViewCodeNamespace.AppViewCode");
				if (type == null)
				{
					Console.WriteLine("AppViewCodeNamespace.AppViewCode class was not found!");
					throw new NullReferenceException("AppViewCodeNamespace.AppViewCode class was not found!");
				}

				IView view = (IView)Activator.CreateInstance(type);
				if (view == null)
				{
					Console.WriteLine("AppViewCodeNamespace.AppViewCode class cannot be instanciated!");
					throw new NullReferenceException("AppViewCodeNamespace.AppViewCode class cannot be instanciated!");
				}

				return view;
			}
		}

		private string GetCSharpCode(string viewContent)
		{
			string[] lines = viewContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			StringBuilder cSharpCode = new StringBuilder();
			string[] supportedOperators = new[] { "for", "if", "else" };
			string cSharpCodePattern = @"@[^\s<\""\/]+";
			Regex cSharpCodeRegex = new Regex(cSharpCodePattern);

			foreach (var line in lines)
			{
				if (line.TrimStart().StartsWith("{") || line.TrimStart().StartsWith("}"))
				{
					cSharpCode.AppendLine(line);
				}
				else if (supportedOperators.Any(x => line.TrimStart().StartsWith("@" + x)))
				{
					int atSignIndex = line.IndexOf('@');
					string cSharpLine = line.Remove(atSignIndex, 1);
					cSharpCode.AppendLine(cSharpLine);
				}
				else
				{
					if (!line.Contains("@"))
					{
						string cSharpLine = $"html.AppendLine(@\"{line.Replace("\"", "\"\"")}\");";
						cSharpCode.AppendLine(cSharpLine);
					}
					else
					{
						string cSharpLine = "html.AppendLine(@\"";
						string restOfLine = line;

						while (restOfLine.Contains("@"))
						{
							int atSsignLocation = restOfLine.IndexOf("@");
							string plainText = restOfLine.Substring(0, atSsignLocation).Replace("\"", "\"\"");
							string cSharpExpression = cSharpCodeRegex.Match(restOfLine)?.Value?.Substring(1);

							cSharpLine += plainText + "\" + " + cSharpExpression + " + @\"";
							int parsedLineLength = atSsignLocation + cSharpExpression.Length + 1;
							if (restOfLine.Length <= parsedLineLength)
							{
								restOfLine = string.Empty;
							}
							else
							{
								restOfLine = restOfLine.Substring(parsedLineLength);
							}
						}

						cSharpLine += restOfLine.Replace("\"", "\"\"") + "\");";
						cSharpCode.AppendLine(cSharpLine);
					}
				}
			}

			return cSharpCode.ToString().TrimEnd();
		}

		private string GetModelType<T>(T model)
		{
			if(model is IEnumerable)
			{
				string collectionType = model.GetType().Name;
				collectionType = collectionType.Substring(0, collectionType.IndexOf("`"));
				var genericArgument = model.GetType().GetGenericArguments()[0].FullName;
				string modelType = $"{collectionType}<{genericArgument}>";
				return "model as " + modelType;
			}

			return "model as " + model.GetType().FullName;
		}

		private Assembly GetModelAssembly<T>(T model)
		{
			if(model == null)
			{
				return null;
			}

			if(model is IEnumerable)
			{
				return model.GetType().GetGenericArguments()[0].Assembly;
			}
			return model.GetType().Assembly;
		}
	}
}
