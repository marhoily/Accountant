using System;

namespace NewModel.Shared.Annotations
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class VirtualAttribute : Attribute
	{
		public AllowedInheritanceOption Option { get; set; }
		public VirtualAttribute() { }
		public VirtualAttribute(AllowedInheritanceOption option)
		{
			Option = option;
		}
	}
}