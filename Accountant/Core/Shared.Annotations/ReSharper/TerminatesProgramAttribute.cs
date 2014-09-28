using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates that the marked method unconditionally terminates control flow execution.
	/// For example, it could unconditionally throw exception
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [MarkerClass]
    public sealed class TerminatesProgramAttribute : Attribute { }
}