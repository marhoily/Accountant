using System;

namespace NewModel.Shared.Annotations
{
	[MeansIntegrationTestNeeded]
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class ExternalApiServiceAttribute : Attribute
	{
	}
}