using System;

using FluentAssertions;

using NewModel.Shared.Annotations;
using NewModel.Shared.Annotations.ReSharper;
using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests._IntegrationTests_
{
	[TestFixture]
    public sealed class Integration_CombinatorTests
	{
		[DataOnlyObject]
		sealed class Args
		{
			public string Z { get; set; }
		}
		[DataOnlyObject]
		sealed class Result
		{
			public string X { get; set; }
			public string Y { get; set; }
		}
		[UsedImplicitly(ImplicitUseTargetFlags.Members)]
		sealed class Container
		{
			public static string X(string y, string z) { return y + z; }
			public static string Y(string z) { return "2"; }
		}
		[UsedImplicitly(ImplicitUseTargetFlags.Members)]
		sealed class ContainerWithNonStaticMethods
		{
			public string X(string y, string z) { return y + z; }
			static string Y(string z) { return "2"; }
		}
		[UsedImplicitly(ImplicitUseTargetFlags.Members)]
		sealed class ContainerWithArgumentOfWrongType
		{
			public static string X(string y, string z) { return y + z; }
			public static int Y(string z) { return 2; }
		}

		[Test]
		public void Resolve_Should_Return_Correct_Result()
		{
			var result = new Combinator<Args, Container, Result>().Evaluate(new Args { Z = "1" });
			result.Y.Should().Be("2");
			result.X.Should().Be("21");
		}

		[Test]
		public void Evaluate_When_Container_With_Non_Static_Methods_Should_Throw()
		{
			Assert.Throws<ArgumentException>(() => new Combinator<Args, ContainerWithArgumentOfWrongType, Result>().Evaluate(new Args()))
				.Message.Should().Be("X(Y, Z). Object of type 'System.Int32' cannot be converted to type 'System.String'.");
		}
	}
}