﻿namespace MvcFramework.ViewEngine
{
	public interface IView
	{
		string GetHtml(object model);
	}
}
