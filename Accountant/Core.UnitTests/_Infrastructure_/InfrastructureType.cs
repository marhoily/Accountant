using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using NewModel.Shared.Annotations;
using NewModel.Shared.Utils;

using NUnit.Framework;

namespace NewModel.UnitTests._Infrastructure_
{
    public sealed class InfrastructureType : IInfrastructureType
    {
        readonly Assembly mTestAssembly;
        public TypeSource Source { get; private set; }

        public Type TargetType { get; private set; }
        public Type TestType { get { return GetUnitTest(TargetType, mTestAssembly); } }

        public bool IsVirtual { get { return TargetType.HasAttribute<VirtualAttribute>(); } }
        public bool IsDataOnly { get { return TargetType.HasAttribute<DataOnlyObjectAttribute>(); } }
        public bool IsUnitTestsNeeded
        {
            get
            {
                return !TargetType
                    .GetCustomAttributes(false)
                    .Any(x => AttributesThatMeanNoUnitTestsNeeded.Contains(x.GetType()));
            }
        }
		public bool IsTestExtensionClass {get { return TargetType.GetCustomAttributes(false).Any(x => x is TestExtensionsAttribute); }}
	    public bool IsTestFixture
	    {
		    get { return TargetType.HasAttribute<TestFixtureAttribute>(); }
	    }
	    public bool HasPublicMethods
	    {
		    get { return TargetType.HasPublicMethods(); }
	    }
	    public bool HasPublicMethodsOtherThanToString
	    {
			get { return TargetType.HasPublicMethods(new[] { "ToString" }); }
	    }
        public bool IsCompilerGenerated
        {
            get
            {
                return TargetType.FullName.Contains("JetBrains.Profiler.Core.Instrumentation")
                    || TargetType.HasAttribute<CompilerGeneratedAttribute>();
            }
        }
        public bool HasSpecialNamespace
        {
            get
            {
                return TargetType.Namespace != null &&
                       TargetType.Namespace.Split('.').Any(part =>
                          part.StartsWith("_") && part.EndsWith("_"));
            }
        }

        public InfrastructureType(Type testType)
        {
            Source = TypeSource.TestAssembly;
            TargetType = testType;
        }
        public InfrastructureType(Type codeType, Assembly testAssembly)
        {
            mTestAssembly = testAssembly;
            Source = TypeSource.CodeAssembly;
            TargetType = codeType;
        }

        public override string ToString()
        {
            return TargetType.ToString();
        }

        public void Check()
        {
            Debug.WriteLine(TargetType.ToString());

            if (Source == TypeSource.CodeAssembly)
                if (IsUnitTestsNeeded)
                    if (!TargetType.IsEnum && !TargetType.IsInterface)
                        if (TargetType.IsNestedPublic || !TargetType.IsNested)
							if (!TargetType.Namespace.EndsWith("Migrations"))
								if (TestType == null)
									Assert.Fail("Every code type should have test " +
										"\r\nor should be marked with one of the attributes " +
										string.Join("\r\n", AttributesThatMeanNoUnitTestsNeeded
										.Select(x => "[" + x.Name.Replace("Attribute", "") + "]")));

            if (Source == TypeSource.TestAssembly)
                if (TargetType.IsClass && !TargetType.IsNested && !HasSpecialNamespace && !IsTestExtensionClass)
                {
                    Assert.That(TargetType.IsPublic,
                        "Every test type should be public");

                    Assert.That(IsTestFixture,
                        "Every test type should have [TestFixture] attribute");

                    Assert.That(TargetType.Name, Is.StringStarting("Test"),
                        "Every test type should have name starting with 'Test'");
                }

            if (TargetType.IsClass && !TargetType.IsAbstract)
                Assert.That(TargetType.IsSealed != IsVirtual,
                    "Every type should be either sealed, or [Virtual], or abstract");

            if (IsDataOnly)
                Assert.That(!HasPublicMethodsOtherThanToString,
                "Every [DataOnly] type should have no public methods other than ToString()");
        }

        static HashSet<Type> sAttributesThatMeanNoUnitTestsNeeded;
        public static HashSet<Type> AttributesThatMeanNoUnitTestsNeeded
        {
            get
            {
                return sAttributesThatMeanNoUnitTestsNeeded ??
                      (sAttributesThatMeanNoUnitTestsNeeded =
                       new HashSet<Type>(
                           typeof(MeansNoUnitTestsNeededAttribute).Assembly.GetTypes()
                           .Where(x => x.HasAttribute<MeansNoUnitTestsNeededAttribute>())
						   .Concat(new[] { typeof(GeneratedCodeAttribute) })
                           .ToList()));
            }
        }
        static HashSet<Type> sAttributesThatMeanIntegrationTestNeeded;
        public static HashSet<Type> AttributesThatMeanIntegrationTestNeeded
        {
            get
            {
                return sAttributesThatMeanIntegrationTestNeeded ??
                      (sAttributesThatMeanIntegrationTestNeeded =
                       new HashSet<Type>(
                           typeof(MeansIntegrationTestNeededAttribute).Assembly.GetTypes()
                           .Where(x => x.HasAttribute<MeansIntegrationTestNeededAttribute>())
                           .ToList()));
            }
        }

        private static Type GetUnitTest(Type type, Assembly unitTestsAssembly)
        {
            var ns = type.Namespace;
            if (ns == null) return null;
            var root = SubstringOnLeftOfFirst(type.Assembly.FullName, ",");
            var replace = ns.Replace(root, String.Format("{0}.UnitTests", root));
            var className = SubstringOnLeftOfFirst(type.Name, "`");
            var name = replace + ".Test" + className;
            Debug.WriteLine("Looking for: " + name);
            return unitTestsAssembly.GetType(name);
        }

        private static string SubstringOnLeftOfFirst(string str, string sub)
        {
            var idx = str.IndexOf(sub, StringComparison.Ordinal);
            return idx != -1 ? str.Substring(0, idx) : str;
        }
    }
}