using System.Collections.Generic;

using FakeItEasy;

using FluentAssertions;

using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FunctionsCombinator
{
	[TestFixture]
    public sealed class CombinatorFactoriesTests
	{
		sealed class Args { }
		sealed class Container { }

		CombinatorFactories mUnderTest;

		[SetUp]
		public void SetUp()
		{
			mUnderTest = new CombinatorFactories();
		}

		[Test]
		public void CreateCalculator_Should_Create_Calculator()
		{
			var expectedParts = new Dictionary<string, Part>();
			var calculator = (Calculator)mUnderTest.CreateCalculator(expectedParts);
			object parts = calculator.Parts;
			parts.Should().BeSameAs(expectedParts);
		}
		[Test]
		public void CreateResultComposer_Should_Create_ResultComposer()
		{
			mUnderTest.CreateResultComposer().Should().BeOfType<ResultComposer>();
		}
		[Test]
		public void CreatePartsExtractor_Should_Create_ResultComposer()
		{
			var args = new Args();
			var partsExtractor = (PartsExtractor)mUnderTest.CreatePartsExtractor(args);
			partsExtractor.Args.Should().Be(args);
		}
		[Test]
		public void ExtractParts_Should_Extract_Parts()
		{
			var partsExtractor = A.Fake<IPartsExtractor>();

			var a = new Part { Contract = "a" };
			var b = new Part { Contract = "b" };
			A.CallTo(() => partsExtractor.ExtractArgs<Args>()).Returns(new[] { a });
			A.CallTo(() => partsExtractor.ExtractActors<Container>()).Returns(new[] { b });

			mUnderTest.ExtractParts<Container, Args>(partsExtractor).ShouldBeEquivalentTo(
				new Dictionary<string, Part> { { "a", a }, { "b", b } });
		}
	}
}