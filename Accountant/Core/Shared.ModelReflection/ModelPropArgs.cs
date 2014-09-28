using System;
using System.Reflection;

using NewModel.Shared.Annotations;
using NewModel.Shared.FunctionsCombinator;
using NewModel.Shared.Utils;

namespace NewModel.Shared.ModelReflection
{
    [DataOnlyObject]
    public sealed class ModelPropArgs
    {
        public ITypeNameFormatter TypeNameFormatter { get; set; }
        public PropertyInfo ReflectedProperty { get; set; }
        public Type ReflectedType { get; set; }
        public Assembly ReflectedAssembly { get; set; }
        public IDependencyResolver DependencyResolver { get; set; }
    }
}