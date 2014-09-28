using System.Collections.Generic;

using FluentAssertions;

using NewModel.Shared.Utils;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.Utils
{
	[TestFixture]
    public sealed class TypeNameFormatterTests
	{
		ITypeNameFormatter mUnderTest;

		[SetUp]
		public void Setup()
		{
			mUnderTest = new TypeNameFormatter();
		}
		
		[Test]
		public void GetReadableName_Of_CommonType_Should_Be_That_Type_Name()
		{
            mUnderTest.GetReadableName(typeof(TypeNameFormatter))
                      .Should().Be("TypeNameFormatter");
		}
		[Test]
		public void GetReadableName_Of_Primitive_Should_Be_CSharp_Equivalent()
		{
			mUnderTest.GetReadableName(typeof(int)).Should().Be("int");
		}
		[Test]
		public void GetReadableName_Generic_Should_Be_CSharpNotation()
		{
			mUnderTest.GetReadableName(typeof(List<int>)).Should().Be("List<int>");
		}
		[Test]
		public void GetReadableName_Generic_Should_Be_CSharpNotation1()
		{
            mUnderTest.GetReadableName(typeof(List<TypeNameFormatter>))
                      .Should().Be("List<TypeNameFormatter>");
		}
		[Test]
		public void GetReadableName_Nullable_Should_Be_CSharpNotation1()
		{
			mUnderTest.GetReadableName(typeof(int?)).Should().Be("int?");
		}
		[Test]
		public void GetReadableName_Multiple_Parameters_Should_Be_CSharpNotation1()
		{
			mUnderTest.GetReadableName(typeof(Dictionary<int?, List<int>>))
			          .Should().Be("Dictionary<int?, List<int>>");
		}
	}
}