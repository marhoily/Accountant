using System.Linq;

using FluentAssertions;

using NewModel.Shared.Annotations;
using NewModel.Shared.Annotations.ReSharper;
using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FunctionsCombinator
{
	[TestFixture]
    public sealed class PartsExtractorTests
	{
		PartsExtractor mUnderTest;
		[DataOnlyObject]
		sealed class Args
		{
			public string Z { get; set; }
		}
		sealed class Container
		{
			[UsedImplicitly("by reflection")]
			public static string X(string y, string z) { return y + z; }
		}

		[SetUp]
		public void SetUp()
		{
			mUnderTest = new PartsExtractor(new Args { Z = "z" });
		}

		[Test]
		public void Extract_For_Args_Should_Work()
		{
			var part = mUnderTest.ExtractArgs<Args>().Single();
			part.Contract.Should().Be("Z");
			part.DependsOn.Should().BeEmpty();
			part.IsCalculated.Should().BeFalse();
			part.Value.Should().BeNull();
			part.Action(new object[0]).Should().Be("z");
		}

		[Test]
		public void Extract_For_Container_Should_Work()
		{
			var part = mUnderTest.ExtractActors<Container>().Single();
			part.Contract.Should().Be("X");
			part.DependsOn.Should().Equal(new object[] {"Y", "Z"});
			part.IsCalculated.Should().BeFalse();
			part.Value.Should().BeNull();
			part.Action(new object[]{"1", "2"}).Should().Be("12");
		}
	}
}