using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates that the value of marked type (or its derivatives) cannot be compared using '==' or '!=' operators.
	/// There is only exception to compare with <c>null</c>, it is permitted
	/// </summary>
	[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false,
		Inherited = true)]
    [MarkerClass]
    public sealed class CannotApplyEqualityOperatorAttribute : Attribute { }
}