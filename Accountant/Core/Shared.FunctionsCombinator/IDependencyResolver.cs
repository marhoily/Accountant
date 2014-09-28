using System.Collections.Generic;
using System.Reflection;

namespace NewModel.Shared.FunctionsCombinator
{
	public interface IDependencyResolver
	{
		IEnumerable<Assembly> GetDependentReferences(Assembly assembly);
	}
}