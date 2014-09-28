using System;
using System.Reflection;

namespace NewModel.Shared.Annotations
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    [MarkerClass]
    public sealed class SerializableMemberAttribute : Attribute
	{
		public string Name { get; set; }
		public SerializableMemberAttribute() {}

		public SerializableMemberAttribute(string name)
		{
			Name = name;
		}

		public string GetName(MemberInfo memberInfo)
		{
			return Name ?? memberInfo.Name;
		}
	}
}