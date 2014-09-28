using System;

using NewModel.Shared.Annotations.ReSharper;

namespace NewModel.Shared.Annotations
{
	/// <summary> Allows properties and ctor-s only </summary>
	[MeansNoUnitTestsNeeded]
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	[MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    [MarkerClass]
	public sealed class DataOnlyObjectAttribute : Attribute
	{
	}
}