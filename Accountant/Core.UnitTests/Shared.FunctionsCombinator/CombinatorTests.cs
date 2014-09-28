using System.Collections.Generic;

using FakeItEasy;

using FluentAssertions;

using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FunctionsCombinator
{
	[TestFixture]
    public sealed class CombinatorTests
	{
		sealed class Args { }
		sealed class Result { }
		sealed class Container { }

		Combinator<Args, Container, Result> mUnderTest;
		ICombinatorFactories mCombinatorFactories;
		readonly Args mArgs = new Args();

		[SetUp]
		public void SetUp()
		{
			mCombinatorFactories = A.Fake<ICombinatorFactories>();
			mUnderTest = new Combinator<Args, Container, Result>(mCombinatorFactories);
		}

		[Test]
		public void Evaluate_Should_CreateExtractor_CreateResultComposer_ExtractParts_CreateCalculator_And_Compose_Result()
		{
			var extractor = A.Fake<IPartsExtractor>();
			var resultComposer = A.Fake<IResultComposer>();
			var parts = new Dictionary<string, Part>();
			var calculator = A.Fake<ICalculator>();
			var result = new Result();

			A.CallTo(() => mCombinatorFactories.CreatePartsExtractor(mArgs)).Returns(extractor);
			A.CallTo(() => mCombinatorFactories.CreateResultComposer()).Returns(resultComposer);
			A.CallTo(() => mCombinatorFactories.ExtractParts<Container, Args>(extractor)).Returns(parts);
			A.CallTo(() => mCombinatorFactories.CreateCalculator(parts)).Returns(calculator);
			A.CallTo(() => resultComposer.Compose<Result>(calculator)).Returns(result);
			mUnderTest.Evaluate(mArgs).Should().Be(result);

		}
	}
}