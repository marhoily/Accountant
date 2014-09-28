using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	/// <summary>
	/// Indicates the condition parameter of the assertion method. 
	/// The method itself should be marked by <see cref="AssertionMethodAttribute"/> attribute.
	/// The mandatory argument of the attribute is the assertion type.
	/// </summary>
	/// <seealso cref="AssertionConditionType"/>
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    [MarkerClass]
    public sealed class AssertionConditionAttribute : Attribute
	{
		/// <summary>
		/// Initializes new instance of AssertionConditionAttribute
		/// </summary>
		/// <param name="conditionType">Specifies condition type</param>
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			ConditionType = conditionType;
		}

		/// <summary>
		/// Gets condition type
		/// </summary>
		public AssertionConditionType ConditionType { get; private set; }
	}
}