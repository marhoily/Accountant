using System;
using System.Collections.Generic;
using System.Linq;

namespace NewModel.Shared.Utils
{
	public sealed class TypeNameFormatter : ITypeNameFormatter
	{
		static readonly Dictionary<Type, string> sBuiltinReadableNames = new Dictionary<Type, string>
			{
				// ReSharper disable StringLiteralTypo
				{typeof (bool), "bool"},
				{typeof (byte), "byte"},
				{typeof (short), "short"},
				{typeof (ushort), "ushort"},
				{typeof (char), "char"},
				{typeof (int), "int"},
				{typeof (uint), "uint"},
				{typeof (long), "long"},
				{typeof (ulong), "ulong"},
				{typeof (float), "float"},
				{typeof (double), "double"},
				{typeof (decimal), "decimal"},
				{typeof (string), "string"},
				{typeof (object), "object"},
				// ReSharper restore StringLiteralTypo
			};

		public string GetReadableName(Type type)
		{
			string result;

			if (sBuiltinReadableNames.TryGetValue(type, out result))
				return result;

			if (!type.IsGenericType)
				return type.Name;

			if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
				return string.Concat(GetReadableName(type.GetGenericArguments()[0]), "?");

			return string.Concat(type.Name.Split('`')[0],
				"<", string.Join(", ", type.GetGenericArguments().Select(GetReadableName)), ">");
		}
	}
}