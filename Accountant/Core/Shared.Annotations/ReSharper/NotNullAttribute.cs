using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates that the value of marked element could never be <c>null</c>
	/// </summary>
	[AttributeUsage(
		AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate |
		AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [MarkerClass]
    public sealed class NotNullAttribute : Attribute { }
}