using System;

namespace NewModel.Shared.Annotations
{
    /// <summary>Static class that is has only constants, static read-only fields, and nested classes</summary>
    [MeansNoUnitTestsNeeded]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class NamespaceClassAttribute : Attribute
    {
    }
}