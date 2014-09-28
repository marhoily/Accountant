using System;

namespace NewModel.Shared.Annotations
{
    /// <summary>
    /// Marks an extension class in a test assembly
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class TestExtensionsAttribute : Attribute { }
}