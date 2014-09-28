using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Parameter)]
    [MarkerClass]
    public sealed class AspMvcAreaAttribute : PathReferenceAttribute
	{
		[UsedImplicitly]
		public string AnonymousProperty { get; private set; }

		[UsedImplicitly]
		public AspMvcAreaAttribute() {}

		public AspMvcAreaAttribute(string anonymousProperty)
		{
			AnonymousProperty = anonymousProperty;
		}
	}
}