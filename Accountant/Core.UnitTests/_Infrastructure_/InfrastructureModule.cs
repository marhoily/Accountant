using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace NewModel.UnitTests._Infrastructure_
{
	public sealed class InfrastructureModule : List<IInfrastructureType>
	{
		public Assembly CodeAssembly { get; private set; }
		public Assembly TestAssembly { get; private set; }

		#region ' Ugly nested class '
		sealed class CodeAssemblyNotFound : IInfrastructureType
		{
			readonly string mReplace;

			public CodeAssemblyNotFound(string replace)
			{
				mReplace = replace;
			}

			public void Check()
			{
				Assert.Fail("No '" + mReplace + "' library is found. Perhaps it's never accessed from code explicitly");
			}

			public override string ToString()
			{
				return "Not found: " + mReplace;
			}
		}
		#endregion


		InfrastructureModule(Assembly testAssembly)
		{
			TestAssembly = testAssembly;

			var codeAssembly = GetCodeAssemblyForTestAssembly(testAssembly);
			if (codeAssembly == null) return;
			CodeAssembly = codeAssembly;

			AddRange(from t in CodeAssembly.GetTypes()
					 select new InfrastructureType(t, TestAssembly));

			AddRange(from t in TestAssembly.GetTypes()
					 select new InfrastructureType(t));

			RemoveAll(t => ((InfrastructureType)t).IsCompilerGenerated);
		}

		Assembly GetCodeAssemblyForTestAssembly(Assembly testAssembly)
		{
			var replace = new AssemblyName(testAssembly.FullName).Name.Replace(".UnitTests", "");
			var assemblyName = testAssembly.GetReferencedAssemblies().SingleOrDefault(r => r.Name == replace);
			if (assemblyName == null)
			{
				Add(new CodeAssemblyNotFound(replace));
				return null;
			}

			return Assembly.Load(assemblyName);
		}

		/// <typeparam name="TTest">Any type from TestAssembly</typeparam>
		public static InfrastructureModule FromTypes<TTest>()
		{
			return new InfrastructureModule(typeof(TTest).Assembly);
		}
	}
}