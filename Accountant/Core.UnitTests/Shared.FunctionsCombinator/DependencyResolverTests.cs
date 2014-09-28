using NewModel.Shared.FunctionsCombinator;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FunctionsCombinator
{
	[TestFixture]
    public sealed class DependencyResolverTests
	{
		DependencyResolver mUnderTest;

		[SetUp]
		public void SetUp()
		{
			mUnderTest = new DependencyResolver();
		}

	}
}