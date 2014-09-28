using System;

namespace NewModel.Shared.Annotations
{
	[MeansNoUnitTestsNeeded]
	[MeansIntegrationTestNeeded]
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class EntityFrameworkSpecificAttribute : Attribute
	{
	}
}