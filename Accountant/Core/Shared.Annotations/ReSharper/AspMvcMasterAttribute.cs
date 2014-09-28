using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Parameter)]
    [MarkerClass]
    public sealed class AspMvcMasterAttribute : Attribute { }
}