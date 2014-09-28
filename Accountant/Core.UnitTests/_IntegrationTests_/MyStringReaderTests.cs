using FluentAssertions;

using NUnit.Framework;

namespace NewModel.UnitTests._IntegrationTests_
{
    [TestFixture]
    public sealed class MyStringReaderTests
    {
        MyStringReader mUnderTest;

        [SetUp]
        public void SetUp()
        {
            mUnderTest = new MyStringReader("sample example");
        }

        [Test]
        public void ReadToTheEnd_Should_Start_At_Position_And_Stop_At_Tail()
        {
            mUnderTest.Position = 1;
            mUnderTest.TailLength = 1;
            mUnderTest.ReadToTheEnd(2).Should().Be("ample exampl");
        }

        [Test]
        public void ReadToTheEnd_When_Variable_Is_Too_Long_Should_Return_Null()
        {
            mUnderTest.Position = 1;
            mUnderTest.TailLength = 1;
            mUnderTest.ReadToTheEnd(20).Should().Be(null);
        }


        [Test]
        public void ReadVariable_Should_Return_String_Before_Terminator()
        {
            mUnderTest.ReadVariable(2, " example").Should().Be("sample");
        }
        [Test]
        public void ReadVariable_Should_Move_Position()
        {
            mUnderTest.ReadVariable(2, " example");
            mUnderTest.Position.Should().Be(6);
        }
//        [Test]
//        public void ReadVariable_Should_Move_Tail()
//        {
//            mUnderTest.ReadVariable(2, " example");
//            mUnderTest.Position.Should().Be(6);
//        }
        [Test]
        public void ReadVariable_Should_Not_Return_String_Shorter_Than_VariableLength()
        {
            mUnderTest.ReadVariable(2, "ample ").Should().BeNull();
        }
        [Test]
        public void ReadVariable_Should_Start_From_Position()
        {
            mUnderTest.Position = 1;
            mUnderTest.ReadVariable(2, "ple ").Should().Be("am");
        }
        [Test]
        public void ReadVariable_When_Ambiguous_Should_Return_Null()
        {
            mUnderTest.ReadVariable(2, "ple").Should().BeNull();
        }
        [Test]
        public void ReadVariable_When_First_Match_Would_Be_Too_Short_Should_Not_Be_Ambiguous()
        {
            mUnderTest.ReadVariable(4, "ple").Should().Be("sample exam");
        }
        [Test]
        public void ReadVariable_When_Unsuccessful_Should_Not_Move_Position()
        {
            mUnderTest.ReadVariable(1, "blah");
            mUnderTest.Position.Should().Be(0);
        }
        [Test]
        public void ReadVariable_Should_Respect_Tail_Shorter_Than_Variable_Size()
        {
            // Demonstrate
            mUnderTest.Position = 1;
            mUnderTest.TailLength = 1;
            mUnderTest.ReadVariable(2, "ple exampl").Should().Be("am");
            // Arrange
            mUnderTest.Position = 1;
            mUnderTest.TailLength = 2;
            // Act, Assert
            mUnderTest.ReadVariable(2, "ple exampl").Should().Be(null);
        }
        [Test]
        public void ReadVariable_Should_Respect_Tail_When_Matching_Constant()
        {
            mUnderTest.TailLength = 1;
            mUnderTest.ReadVariable(2, " example").Should().BeNull();
        }
        [Test]
        public void ReadVariable_When_Determining_Ambiguity_Should_Respect_TailLength()
        {
            mUnderTest.TailLength = 1;
            mUnderTest.ReadVariable(2, "ple").Should().Be("sam");
        }

        [Test]
        public void ReadConstant_Should_Start_From_Position()
        {
            mUnderTest.Position = 7;
            mUnderTest.ReadConstant("example").Should().BeTrue();
        }
        [Test]
        public void ReadConstant_Should_Move_The_Reader_Forward()
        {
            mUnderTest.ReadConstant("sample");
            mUnderTest.Position.Should().Be(6);
        }
        [Test]
        public void ReadConstant_Should_Return_True()
        {
            mUnderTest.ReadConstant("sample").Should().BeTrue();
        }
        [Test]
        public void ReadConstant_When_Wrong_Constant_Should_Return_False()
        {
            mUnderTest.ReadConstant("wrong").Should().BeFalse();
        }
        [Test]
        public void ReadConstant_When_Wrong_Constant_Should_Not_Move_Position()
        {
            mUnderTest.ReadConstant("wrong");
            mUnderTest.Position.Should().Be(0);
        }
        [Test]
        public void ReadConstant_When_Constant_Is_Too_Long_Should_Return_False()
        {
            mUnderTest.ReadConstant("sample string and then some").Should().BeFalse();
        }
    }
}