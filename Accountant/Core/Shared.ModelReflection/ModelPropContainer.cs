using System.Reflection;

using NewModel.Shared.Annotations.ReSharper;
using NewModel.Shared.Attributes;
using NewModel.Shared.Utils;

namespace NewModel.Shared.ModelReflection
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers /* By FunctionsCombinator */)]
    public sealed class ModelPropContainer
    {
        public enum PropertyKind
        {
            NavigationSimple, NavigationList, CompoundList, CompoundSimple, Simple
        }
        public static bool IsNavigation(PropertyInfo reflectedProperty)
        {
            return reflectedProperty.IsDefined(typeof(NavigationAttribute));
        }
        public static bool IsCompound(PropertyInfo reflectedProperty)
        {
            return reflectedProperty.IsDefined(typeof(CompoundAttribute));
        }
        public static string PropertyTypeName(PropertyInfo reflectedProperty, ITypeNameFormatter typeNameFormatter)
        {
            return typeNameFormatter.GetReadableName(reflectedProperty.PropertyType);
        }
        public static string PropertyItemTypeName(PropertyInfo reflectedProperty, ITypeNameFormatter typeNameFormatter)
        {
            var itemType = reflectedProperty.PropertyType.ItemType();
            if (itemType == null) return "";
            return typeNameFormatter.GetReadableName(itemType);
        }
      
        public static PropertyKind PropKind(PropertyInfo reflectedProperty, bool isNavigation, bool isCompound)
        {
            var isList = reflectedProperty.PropertyType.ItemType() != null;

            if (isNavigation) return isList
                ? PropertyKind.NavigationList
                : PropertyKind.NavigationSimple;

            if (isCompound) return isList
                ? PropertyKind.CompoundList
                : PropertyKind.CompoundSimple;

            return PropertyKind.Simple;
        }


        public static string DefinitionPattern(PropertyKind propKind)
        {
            switch (propKind)
            {
                case PropertyKind.NavigationList:
                    return "public string[] PropName { get; set; }";
                case PropertyKind.NavigationSimple:
                    return "public string PropName { get; set; }";
                case PropertyKind.CompoundList:
                    return "public List<PropItemTypeData> PropName { get; set; }";
                case PropertyKind.CompoundSimple:
                    return "public PropTypeData PropName { get; set; }";
                default:
                    return "public PropType PropName { get; set; }";
            }
        }
        public static string InitializerPattern(PropertyKind propKind)
        {
            switch (propKind)
            {
                case PropertyKind.NavigationList:
                    return "PropName = value.PropName.Select(x => x.Name).ToArray()";
                case PropertyKind.NavigationSimple:
                    return "PropName = value.PropName == null ? null : value.PropName.Name";
                case PropertyKind.CompoundList:
                    return "PropName = value.PropName.Select(x => new PropItemTypeData(x)).ToList()";
                case PropertyKind.CompoundSimple:
                    return "PropName = value.PropName == null ? null : new PropTypeData(value.PropName)";
                default:
                    return "PropName = value.PropName";
            }
        }
        public static string FinalizerPattern(PropertyKind propKind)
        {
            switch (propKind)
            {
                case PropertyKind.NavigationList:
                    return "PropName = PropName.Select(x => context.PropItemTypeByKey[x]).ToList()";
                case PropertyKind.NavigationSimple:
                    return "PropName = PropName == null ? null : context.PropTypeByKey[PropName]";
                case PropertyKind.CompoundList:
                    return "PropName = new List(PropName.Select(x => x.Back(context)))";
                case PropertyKind.CompoundSimple:
                    return "PropName = PropName.Back(context)";
                default:
                    return "PropName = PropName";
            }
        }

        public static string Definition(string definitionPattern, 
            PropertyInfo reflectedProperty, 
            string propertyTypeName, 
            string propertyItemTypeName)
        {
            return definitionPattern
                .Replace("new List", "new " + propertyTypeName)
                .Replace("PropName", reflectedProperty.Name)
                .Replace("PropType", propertyTypeName)
                .Replace("PropItemType", propertyItemTypeName);
        }
        public static string Initializer(string initializerPattern,
            PropertyInfo reflectedProperty, 
            string propertyTypeName, 
            string propertyItemTypeName)
        {
            return initializerPattern
                .Replace("new List", "new " + propertyTypeName)
                .Replace("PropName", reflectedProperty.Name)
                .Replace("PropType", propertyTypeName)
                .Replace("PropItemType", propertyItemTypeName);
        }
        public static string Finalizer(string finalizerPattern,
            PropertyInfo reflectedProperty, 
            string propertyTypeName, 
            string propertyItemTypeName)
        {
            return finalizerPattern
                .Replace("new List", "new " + propertyTypeName)
                .Replace("PropName", reflectedProperty.Name)
                .Replace("PropType", propertyTypeName)
                .Replace("PropItemType", propertyItemTypeName);
        }

        /*    [Placeholder("Sample")]
            public static string Sample(string samplePattern, 
                PropertyInfo reflectedProperty, 
                string propertyTypeName, 
                string propertyItemTypeName)
            {
                return samplePattern
                    .Replace("PropName", reflectedProperty.Name)
                    .Replace("PropType", propertyTypeName)
                    .Replace("PropItemType", propertyItemTypeName);
            }*/
    }
}