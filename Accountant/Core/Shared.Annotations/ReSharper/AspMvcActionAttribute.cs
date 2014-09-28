using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    [MarkerClass]
    public sealed class AspMvcActionAttribute : Attribute
	{
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }
		public AspMvcActionAttribute() {}

		public AspMvcActionAttribute(string anonymousProperty)
		{
			AnonymousProperty = anonymousProperty;
		}
	}
}