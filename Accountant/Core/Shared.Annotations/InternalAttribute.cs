using System;

namespace NewModel.Shared.Annotations
{
    /// <summary>
    /// Internal class that is fully covered in a public class that uses it
    /// </summary>
    [MeansNoUnitTestsNeeded]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class InternalAttribute : Attribute
    {
    }
}