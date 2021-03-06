﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using MvcFramework.Identity;
using MvcFramework.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MvcFramework.ViewEngine
{
	public class SisViewEngine : IViewEngine
	{
		public string GetHtml<T>(string viewContent, T model, ModelStateDictionary modelState, Principal user = null)
		{
			string cSharpCode = CheckForWidgets(viewContent);
			cSharpCode = GetCSharpCode(cSharpCode);

			string code = $@"
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MvcFramework.ViewEngine;
using MvcFramework.Identity;
using MvcFramework.Validation;

namespace AppViewCodeNamespace
{{
	public class AppViewCode : IView
	{{
		public string GetHtml(object model, ModelStateDictionary modelState, Principal user)
		{{
			var Model = {(model == null ? "new {}" : GetModelType(model))};
			var User = user;
			var ModelState = modelState;
			
			var html = new StringBuilder();
			{cSharpCode}
			return html.ToString();
		}}
	}}
}}";

			IView view = CompileAndInstance(code, GetModelAssembly(model));
			var html = view.GetHtml(model, modelState, user).TrimEnd();
			return html;
		}

		private string CheckForWidgets(string viewContent)
		{
			List<IViewWidget> widgets = Assembly.GetEntryAssembly()
				.GetTypes()
				.Where(type => typeof(IViewWidget).IsAssignableFrom(type))
				.Select(type => (IViewWidget)Activator.CreateInstance(type))
				.ToList();

			if(widgets == null || widgets.Count == 0)
			{
				return viewContent;
			}

			foreach (var widget in widgets)
			{
				viewContent = viewContent.Replace($"@Widgets.{widget.GetType().Name}", widget.Render());
			}

			return viewContent;
		}

		private IView CompileAndInstance(string code, Assembly modelAssembly)
		{
			modelAssembly = modelAssembly ?? Assembly.GetEntryAssembly();

			CSharpCompilation compilation = CSharpCompilation.Create("AppViewModel")
				.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
				.AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
				.AddReferences(MetadataReference.CreateFromFile(modelAssembly.Location))
				.AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

			var netStandardAssembly = Assembly.Load(new AssemblyName("netstandard"));
			compilation = compilation.AddReferences(MetadataReference.CreateFromFile(netStandardAssembly.Location));
			var netStandardAssemblies = netStandardAssembly.GetReferencedAssemblies();

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
					StringBuilder htmlErrorView = new StringBuilder();
					htmlErrorView.AppendLine($"<h4>{emitResult.Diagnostics.Count()} errors:</h4>");

					foreach (var error in emitResult.Diagnostics)
					{
						Console.WriteLine(error.GetMessage());
						htmlErrorView.AppendLine($"<p>{error.GetMessage()}</p>");
					}

					htmlErrorView.AppendLine($"<pre>{WebUtility.HtmlEncode(code)}</pre>");
					return new ErrorView(htmlErrorView.ToString());
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
			string[] lines = viewContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder cSharpCode = new StringBuilder();
			string[] supportedOperators = new[] { "for", "if", "else" };
			string cSharpCodePattern = @"@[^\s<\""\/&]+";
			string cSharpInlineCodePattern = @"@{[^}]+}";
			Regex cSharpCodeRegex = new Regex(cSharpCodePattern);
			Regex cSharpInlineCodeRegex = new Regex(cSharpInlineCodePattern);
			bool isCSharpBlockCode = false;
			int cSharpCodeDepth = 0;

			foreach (var line in lines)
			{
				//TODO: Write test for this
				if (line.Trim().StartsWith("@{") && line.Trim().EndsWith("}"))
				{
					string cSharpLine = line.Trim();
					//@{ var name = "Atanas" }
					cSharpCode.AppendLine(cSharpLine.Substring(2, cSharpLine.Length - 3));
					continue;
				}

				if (isCSharpBlockCode)
				{
					if(line.Trim().StartsWith("{"))
					{
						cSharpCodeDepth++;
					}
					
					if(line.TrimEnd().EndsWith("}"))
					{
						cSharpCodeDepth--;
						if(cSharpCodeDepth <= 0)
						{
							isCSharpBlockCode = false;
							continue;
						}
					}
					
					cSharpCode.AppendLine(line);
					continue;
				}

				if (line.TrimStart() == "@{")
				{
					isCSharpBlockCode = true;
					cSharpCodeDepth++;
					continue;
				}

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
					string cSharpLine = "html.AppendLine(@\"";
					string restOfLine = line;

					while (restOfLine.Contains("@"))
					{
						int atSsignLocation = restOfLine.IndexOf("@");
						string plainText = restOfLine.Substring(0, atSsignLocation).Replace("\"", "\"\"");

						string cSharpExpression = cSharpInlineCodeRegex.Match(restOfLine)?.Value;
						int cSharpExpresssionLength = 0;
						if (!string.IsNullOrEmpty(cSharpExpression))
						{
							cSharpExpresssionLength = cSharpExpression.Length - 1;
							cSharpExpression = cSharpExpression.Substring(2, cSharpExpression.Length - 3);
						}
						else
						{
							cSharpExpression = cSharpCodeRegex.Match(restOfLine)?.Value?.Substring(1);
							cSharpExpresssionLength = cSharpExpression?.Length ?? 0;
						}

						cSharpLine += plainText + "\" + " + cSharpExpression + " + @\"";
						int parsedLineLength = atSsignLocation + cSharpExpresssionLength + 1;
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

			return cSharpCode.ToString().TrimEnd();
		}

		private string GetModelType<T>(T model)
		{
			if (model is IEnumerable)
			{
				string collectionType = model.GetType().Name;
				collectionType = collectionType.Substring(0, collectionType.IndexOf("`"));
				if (collectionType.EndsWith("Iterator"))
				{
					collectionType = "IEnumerable";
				}

				var genericArgument = model.GetType().GetGenericArguments()[0].FullName;
				string modelType = $"{collectionType}<{genericArgument}>";
				return "model as " + modelType;
			}

			return "model as " + model.GetType().FullName;
		}

		private Assembly GetModelAssembly<T>(T model)
		{
			if (model == null)
			{
				return null;
			}

			if (model is IEnumerable)
			{
				return model.GetType().GetGenericArguments()[0].Assembly;
			}
			return model.GetType().Assembly;
		}
	}
}
