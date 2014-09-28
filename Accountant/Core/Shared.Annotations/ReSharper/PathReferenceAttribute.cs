using System;

namespace NewModel.Shared.Annotations.ReSharper
{
	[AttributeUsage(AttributeTargets.Parameter)]
    [MarkerClass]
    [Virtual]
    public class PathReferenceAttribute : Attribute
	{
		public PathReferenceAttribute() {}

		[UsedImplicitly]
		public PathReferenceAttribute([PathReference] string basePath)
		{
			BasePath = basePath;
		}

		[UsedImplicitly]
		public string BasePath { get; private set; }
	}
}