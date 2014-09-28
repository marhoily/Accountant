using System;

using NewModel.Shared.Annotations;

namespace NewModel.Shared.FunctionsCombinator
{
    [MarkerClass]
    [AttributeUsage(AttributeTargets.Method, 
        Inherited = false, AllowMultiple = true)]
    sealed class PlaceholderAttribute : Attribute
    {
        public string Identifier { get; private set; }

        public PlaceholderAttribute(string identifier)
        {
            Identifier = identifier;
        }
    }
}