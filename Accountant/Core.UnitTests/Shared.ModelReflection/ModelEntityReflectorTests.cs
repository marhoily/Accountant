using FakeItEasy;

using NewModel.Shared.FunctionsCombinator;
using NewModel.Shared.ModelReflection;
using NewModel.Shared.Utils;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.ModelReflection
{
    [TestFixture]
    public sealed class ModelEntityReflectorTests
    {
        ModelEntityReflector mUnderTest;

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ModelEntityReflector(A.Fake<ITypeNameFormatter>(), A.Fake<IDependencyResolver>());
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