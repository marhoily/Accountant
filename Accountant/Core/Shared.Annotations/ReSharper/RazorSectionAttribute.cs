using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	// ASP.NET MVC attributes
	// Razor attributes
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, Inherited = true)]
    [MarkerClass]
    public sealed class RazorSectionAttribute : Attribute { }
}