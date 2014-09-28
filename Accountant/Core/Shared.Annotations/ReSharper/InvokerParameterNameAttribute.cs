using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates that the function argument should be string literal and match one  of the parameters of the caller function.
	/// For example, <see cref="ArgumentNullException"/> has such parameter.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    [MarkerClass]
    public sealed class InvokerParameterNameAttribute : Attribute { }
}