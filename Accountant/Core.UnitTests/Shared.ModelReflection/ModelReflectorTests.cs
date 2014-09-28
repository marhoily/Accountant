using ApprovalTests;
using ApprovalTests.Reporters;
using NewModel.Accounting.Persistence;
using NewModel.Shared.ModelReflection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace NewModel.UnitTests.Shared.ModelReflection
{
    [TestFixture]
    [UseReporter(typeof(AraxisMergeReporter))]
    public sealed class ModelReflectorTests
    {
        ModelReflector mUnderTest;

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new ModelReflector();
        }

        [Test]
        public void VerifyReflectLedger()
        {
            var entries = mUnderTest.Reflect(typeof(Ledger));
            Approvals.Verify(
                JsonConvert.SerializeObject(entries, Formatting.Indented));
        }
        [Test]
        public void VerifyReflectBatch()
        {
            var entries = mUnderTest.Reflect(typeof(Batch));
            Approvals.Verify(
                JsonConvert.SerializeObject(entries, Formatting.Indented));
        }
    }

}