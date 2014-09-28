using System;

using NewModel.Shared.Annotations;

namespace NewModel.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class CompoundAttribute : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class NotMappedAttribute : Attribute
    {
    }
}