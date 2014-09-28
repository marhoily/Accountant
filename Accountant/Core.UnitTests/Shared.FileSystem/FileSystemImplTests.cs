using System.IO;

using NewModel.Shared.FileSystem;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FileSystem
{
	[TestFixture]
    public sealed class FileSystemImplTests
	{
		FileSystemImpl mUnderTest;

		[SetUp]
		public void SetUp()
		{
			mUnderTest = new FileSystemImpl();
		}

		[Test]
		public void CreateTempFile_Should_CreateNewFile()
		{
			var tempFile = mUnderTest.CreateTempFile();
			Assert.That(tempFile.Exists, Is.True);
		}

		[Test]
		public void CreateTempFile_WhenDirectory_Should_Create_New_File_In_That_Directory()
		{
			var tempPath = Path.GetTempPath();
			var tempFile = mUnderTest.CreateTempFile(tempPath);
			Assert.That(tempFile.Parent.FullName + @"\", Is.EqualTo(tempPath));
			Assert.That(tempFile.Exists, Is.True);
		}

		[Test]
		public void PathCombine_Should_Work()
		{
			Assert.That(mUnderTest.PathCombine("A", "B"), Is.EqualTo(@"A\B"));
		}

		[Test]
		public void IsPathRooted_Should_Work()
		{
			Assert.That(mUnderTest.IsPathRooted(@"C:\A"));
			Assert.That(!mUnderTest.IsPathRooted(@"C\A"));
		}
	}
}