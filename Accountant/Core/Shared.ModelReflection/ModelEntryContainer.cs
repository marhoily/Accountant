using System;
using System.Linq;

using NewModel.Shared.Annotations.ReSharper;
using NewModel.Shared.Attributes;
using NewModel.Shared.FunctionsCombinator;
using NewModel.Shared.Utils;

namespace NewModel.Shared.ModelReflection
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers /* By FunctionsCombinator */)]
    public sealed class ModelEntryContainer
    {
        static readonly Combinator<ModelPropArgs, ModelPropContainer, ModelProp> 
            sCombinator = new Combinator<ModelPropArgs, ModelPropContainer, ModelProp>();

        public static string Name(Type reflectedType, ITypeNameFormatter typeNameFormatter)
        {
            return typeNameFormatter.GetReadableName(reflectedType);
        }
        public static bool HasKey(Type reflectedType)
        {
            return reflectedType.GetProperty("Name") != null;
        }

        public static ModelProp[] Properties(Type reflectedType, ITypeNameFormatter typeNameFormatter)
        {
            return reflectedType.GetProperties()
                .Where(p => !p.IsDefined(typeof(NotMappedAttribute), false))
                .Select(p => sCombinator.Evaluate(
                    new ModelPropArgs
                    {
                        TypeNameFormatter = typeNameFormatter,
                        ReflectedType = reflectedType,
                        ReflectedProperty = p,
                    }))
                .ToArray();
        }
    }
}