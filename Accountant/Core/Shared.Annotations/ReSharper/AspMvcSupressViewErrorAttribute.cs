using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [MarkerClass]
    public sealed class AspMvcSupressViewErrorAttribute : Attribute { }
}