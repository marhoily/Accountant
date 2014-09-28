using System;

namespace NewModel.Shared.Annotations
{
	/// <summary> Allows ctor-s only </summary>
	[MeansNoUnitTestsNeeded]
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class MarkerClassAttribute : Attribute
	{
	}
}