using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NewModel.Shared.ModelReflection;

namespace NewModel.Shared.Utils
{
	/// <summary>
	/// Provides extension methods for the <see cref="Type"/> class.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>Indicates whether one or more attributes of the specified type or of its derived types is applied to this member.</summary>
		/// <typeparam name="TAttribute">The attribute to look for.</typeparam>
		/// <returns>true if a custom attribute of type attributeType is applied to element; otherwise, false.</returns>
		public static bool HasAttribute<TAttribute>(this Type type)
			where TAttribute : Attribute
		{
			return Attribute.IsDefined(type, typeof(TAttribute));
		}
		/// <summary> Indicates whether there are public methods declared on the type (and not base type)</summary>
		/// <returns>true if any method is located in the specified type; otherwise, false</returns>
		public static bool HasPublicMethods(this Type type, IEnumerable<string> allowedMethodNames = null)
		{
			if (allowedMethodNames == null) allowedMethodNames = new string[] {};
			return type
				.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
				.Where(m => !m.IsSpecialName)
				.Where(m => !allowedMethodNames.Contains(m.Name))
				.Any(m => m.DeclaringType == type);
		}

		/// <summary>
		/// Determines whether the specified type has the specified attribute attached. This method searches implemented interfaces and base classes.
		/// </summary>
		/// <param name="type">The type to look for the attribute in.</param>
		/// <typeparam name="TAttribute">The attribute to look for.</typeparam>
		/// <returns>Returns whether the attribute is attached to the type or any of its ancestors.</returns>
		public static bool HasOrInheritsAttribute<TAttribute>(this Type type)
			where TAttribute : Attribute
		{
			return type != null && type.GetDistinctBaseTypesAndInterfaces().Any(t => t.IsDefined(typeof(TAttribute), true));
		}

		/// <summary>
		/// Returns all types the specified type inherits from, including the type itself.
		/// </summary>
		/// <param name="type">The type to get all ancestors for.</param>
		/// <returns>Returns all types the specified type inherits from, including the type itself.</returns>
		public static IEnumerable<Type> GetDistinctBaseTypesAndInterfaces(this Type type)
		{
            return type.Flatten(GetBaseTypeAndInterfaces);
		}

	    public static IEnumerable<Type> GetBaseTypeAndInterfaces(this Type t)
	    {
            IEnumerable<Type> interfaces = t.GetInterfaces();
            var baseType = t.BaseType;
	        if (baseType == null) return interfaces;
	        interfaces = interfaces.Concat(new[] { baseType });
	        return interfaces;
	    }

	    public static bool IsStatic(this Type type)
		{
			// http://dotneteers.net/blogs/divedeeper/archive/2008/08/04/QueryingStaticClasses.aspx
			return type.IsAbstract && type.IsSealed;
		}

	    public static Type ItemType(this Type type)
	    {
	        var enumerableType = type.GetDistinctBaseTypesAndInterfaces()
                .FirstOrDefault(i => 
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IEnumerable<>));
	        if (enumerableType == null) return null;
	        var genericArguments = enumerableType.GetGenericArguments();
	        if (type.GetMethod("Add", genericArguments) == null) return null;
	        return genericArguments.Single();
	    }
	}
}