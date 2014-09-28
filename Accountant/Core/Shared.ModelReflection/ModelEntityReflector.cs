using System;

using NewModel.Shared.FunctionsCombinator;
using NewModel.Shared.Utils;

namespace NewModel.Shared.ModelReflection
{
    public sealed class ModelEntityReflector
    {
        readonly ITypeNameFormatter mTypeNameFormatter;
        readonly IDependencyResolver mDependencyResolver;

        public ModelEntityReflector(ITypeNameFormatter typeNameFormatter, IDependencyResolver dependencyResolver)
        {
            mTypeNameFormatter = typeNameFormatter;
            mDependencyResolver = dependencyResolver;
        }
        public ModelEntry Reflect(Type reflectedType)
        {
            return new Combinator<ModelEntryArgs, ModelEntryContainer, ModelEntry>()
                .Evaluate(new ModelEntryArgs
                {
                    ReflectedType = reflectedType,
                    TypeNameFormatter = mTypeNameFormatter,
                    ReflectedAssembly = reflectedType.Assembly,
                    DependencyResolver = mDependencyResolver,
                });
        }
    }
}