using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NewModel.Shared.ModelReflection;

namespace NewModel.Shared.FunctionsCombinator
{
	public sealed class DependencyResolver : IDependencyResolver
	{
		public IEnumerable<Assembly> GetDependentReferences(Assembly assembly)
		{
            return assembly.Flatten(mainAssembly => mainAssembly
                .GetReferencedAssemblies()
                .Select(TryLoadAssembly)
                .Where(a => a != null)
                .Where(a => !a.GlobalAssemblyCache));
		}

	    static Assembly TryLoadAssembly(AssemblyName referencedAssembly)
		{
			try { return Assembly.Load(referencedAssembly); }
			catch { return null; }
		}
	}
}