using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    [MarkerClass]
    public sealed class AspMvcControllerAttribute : Attribute
	{
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }
		public AspMvcControllerAttribute() {}

		public AspMvcControllerAttribute(string anonymousProperty)
		{
			AnonymousProperty = anonymousProperty;
		}
	}
}