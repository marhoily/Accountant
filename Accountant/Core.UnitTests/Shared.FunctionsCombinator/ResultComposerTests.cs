using FakeItEasy;

using FluentAssertions;

using NewModel.Shared.Annotations.ReSharper;
using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FunctionsCombinator
{
	[TestFixture]
    public sealed class ResultComposerTests
	{
		ResultComposer mUnderTest;

		[SetUp]
		public void SetUp()
		{
			mUnderTest = new ResultComposer();
		}

		[Test]
		public void Should_Call_Resolve_For_Each_Property()
		{
			var resolver = A.Fake<ICalculator>();
			A.CallTo(() => resolver.Evaluate("X")).Returns(1);
			A.CallTo(() => resolver.Evaluate("Y")).Returns(2);
			mUnderTest.Compose<Result>(resolver)
				.ShouldBeEquivalentTo(new Result { X = 1, Y = 2 });
		}

		[UsedImplicitly(ImplicitUseTargetFlags.Members)]
		sealed class Result
		{
			public int X { get; set; }
			public int Y { get; set; }
		}
	}
}