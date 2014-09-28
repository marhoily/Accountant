using System;

using NewModel.Shared.Annotations;

namespace NewModel.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class NavigationAttribute : Attribute
    {
    }
}