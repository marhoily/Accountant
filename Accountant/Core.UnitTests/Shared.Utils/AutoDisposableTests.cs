using NewModel.Shared.Utils;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.Utils
{
    [TestFixture]
    public sealed class AutoDisposableTests
    {
        AutoDisposable mUnderTest;

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new AutoDisposable(() => {});
        }

        [TearDown]
        public void Dispose() {}

        [Test]
        public void Method()
        {
            Assert.That(mUnderTest, Is.Not.Null);
        }
    }
}