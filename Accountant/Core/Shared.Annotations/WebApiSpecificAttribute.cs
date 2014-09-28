using System;

namespace NewModel.Shared.Annotations
{
    [MeansNoUnitTestsNeeded]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [MarkerClass]
    public sealed class WebApiSpecificAttribute : Attribute
    {
    }
}