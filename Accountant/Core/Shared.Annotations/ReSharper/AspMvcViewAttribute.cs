using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    [MarkerClass]
    public sealed class AspMvcViewAttribute : PathReferenceAttribute { }
}