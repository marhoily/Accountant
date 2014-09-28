using System;
using System.Collections.Generic;

using FluentAssertions;

using NewModel.Shared.ModelReflection;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.ModelReflection
{
    [TestFixture]
    public sealed class HierarchyTests
    {
        [Test]
        public void Flatten_With_FindTwoMultipliers_Should_Find_All_Multipliers()
        {
            Hierarchy.Flatten(10, S)
                .Should().Equal(10, 11, 12, 4, 5, 6, 2, 3, 1);
        }
        [Test]
        public void Flatten_When_Selected_Collection_Contains_Null_Should_Include_Null_Into_Results()
        {
            var source = new object();
            Hierarchy.Flatten(source, o => new object[]{null})
                .Should().Equal(new[] { source, null });
        }
        [Test]
        public void Flatten_When_Selector_Returns_Null_Should_Throw()
        {
            Assert.Throws<NullReferenceException>(
                ()=> Hierarchy.Flatten(new object(), o => null));
        }

        private static IEnumerable<int> S(int i) 
        {
            yield return i%3 == 0 ? i/3 : i + 1;
        }
    }
}