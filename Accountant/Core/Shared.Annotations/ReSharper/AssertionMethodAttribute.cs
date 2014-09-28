using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates that the marked method is assertion method, i.e. it halts control flow if one of the conditions is satisfied. 
	/// To set the condition, mark one of the parameters with <see cref="AssertionConditionAttribute"/> attribute
	/// </summary>
	/// <seealso cref="AssertionConditionAttribute"/>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [MarkerClass]
    public sealed class AssertionMethodAttribute : Attribute { }
}