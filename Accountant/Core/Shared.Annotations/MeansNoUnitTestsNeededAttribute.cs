using System;

namespace NewModel.Shared.Annotations
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class MeansNoUnitTestsNeededAttribute : Attribute
	{
	}
}