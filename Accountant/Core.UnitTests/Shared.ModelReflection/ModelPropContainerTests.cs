using NewModel.Shared.ModelReflection;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.ModelReflection
{
    [TestFixture]
    public sealed class ModelPropContainerTests
    {
        ModelPropContainer mUnderTest;

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ModelPropContainer();
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