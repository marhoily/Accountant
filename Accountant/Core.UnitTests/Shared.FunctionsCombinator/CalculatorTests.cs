using System;
using System.Collections.Generic;
using System.Linq;

using FakeItEasy;

using FluentAssertions;

using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FunctionsCombinator
{
	[TestFixture]
    public sealed class CalculatorTests
	{
		[Test]
		public void Resolve_When_No_Such_Contract_Should_Throw()
		{
			Assert.Throws<Exception>(() =>
				new Calculator(new Dictionary<string, Part>()).Evaluate("x")).
				Message.Should().Be("Cannot resolve: x");
		}
		[Test]
		public void CannotResolve_Exception_Should_Have_Full_Methods_Chain()
		{
			Assert.Throws<Exception>(() =>
				new Calculator(new Dictionary<string, Part>
					{
						{ "x", new Part{ DependsOn = new List<string>{"y", "z"}}},
						{ "y", new Part{ IsCalculated = true}}
					}).Evaluate("x")).
				Message.Should().Be("Cannot resolve: x -> z");
		}
		[Test]
		public void Resolve_When_Calculated_Should_Return_Value()
		{
			new Calculator(new Dictionary<string, Part>
					{
						{ "x", new Part{IsCalculated = true, Value = 35}}
					}).Evaluate("x").Should().Be(35);
		}
		[Test]
		public void Resolve_When_Cyclic_Dependency_Should_Throw()
		{
			Assert.Throws<Exception>(() =>
				new Calculator(new Dictionary<string, Part>{
						{ "x", new Part{ DependsOn = new List<string>{"y"}}},
						{ "y", new Part{ DependsOn = new List<string>{"x"}}}
					}).Evaluate("x")).Message.Should().Be("Cycle found at: x -> y -> x");
		}
		[Test]
		public void Resolve_When_Recursive_Call_Should_Set_IsVisited()
		{
			var action = A.Fake<Func<object[], object>>();
			var part = new Part { Action = action, DependsOn = new List<string>() };
			A.CallTo(() => action(A<object[]>._)).Invokes(x => part.IsVisited.Should().BeTrue());
			new Calculator(new Dictionary<string, Part> { { "x", part } }).Evaluate("x");
			A.CallTo(() => action(A<object[]>._)).MustHaveHappened();
		}
		[Test]
		public void Resolve_When_Recursive_Call_Should_Call_Action()
		{
			var action = A.Fake<Func<object[], object>>();
			var part = new Part { Action = action, DependsOn = new List<string> { "y" } };
			new Calculator(new Dictionary<string, Part>
				{
					{"x", part},
					{"y", new Part {IsCalculated = true, Value = 464}},
				})
				.Evaluate("x");
			A.CallTo(() => action(A<object[]>.That.Matches(a => a.SequenceEqual(new object[] { 464 }))))
				.MustHaveHappened();
		}
		[Test]
		public void Resolve_When_Recursive_Call_Should_Set_Part_Value_And_Flag_As_Calculated()
		{
			var x = new Part {Action = arr => arr[0], DependsOn = new List<string> {"y"}};
			new Calculator(new Dictionary<string, Part>
				{
					{"x", x},
					{"y", new Part {IsCalculated = true, Value = 464}},
				})
				.Evaluate("x");
			x.IsCalculated.Should().BeTrue();
			x.Value.Should().Be(464);
		}
		[Test]
		public void Resolve_When_Recursive_Call_Should_Return_Value()
		{
			new Calculator(new Dictionary<string, Part>
				{
					{"x", new Part {Action = arr => arr[0], DependsOn = new List<string> {"y"}}},
					{"y", new Part {IsCalculated = true, Value = 464}},
				})
				.Evaluate("x").Should().Be(464);
		}
	}
}