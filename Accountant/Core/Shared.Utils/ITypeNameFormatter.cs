using System;

namespace NewModel.Shared.Utils
{
	public interface ITypeNameFormatter {
		string GetReadableName(Type type);
	}
}