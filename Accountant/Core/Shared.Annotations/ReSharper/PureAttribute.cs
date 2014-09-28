using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates that method doesn't contain observable side effects.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true)]
    [MarkerClass]
    public sealed class PureAttribute : Attribute { }
}