using System;
using System.IO;

using FluentAssertions;

using NewModel.Shared.FileSystem;

using NUnit.Framework;

namespace NewModel.UnitTests.Shared.FileSystem
{
	[TestFixture]
    public sealed class FileImplTests
	{
		FileImpl mUnderTest;
		string mTempFile;

		[SetUp]
		public void SetUp()
		{
			mTempFile = Path.GetTempFileName();
			File.Delete(mTempFile);
			mUnderTest = new FileImpl(mTempFile);
		}

		[TearDown]
		public void Dispose()
		{
			if (File.Exists(mTempFile))
			{
				File.Delete(mTempFile);
				mTempFile = null;
			}
		}

		[Test]
		public void OpenXml_Should_Work()
		{
			File.AppendAllText(mTempFile, "<test/>");
			using (var reader = mUnderTest.OpenXml())
			{
				Assert.That(reader.Read());
				Assert.That(reader.Name, Is.EqualTo("test"));
			}
		}

		[Test]
		public void Parent_Should_Work()
		{
			mUnderTest = new FileImpl(@"c:\path\file.txt");
			Assert.That(mUnderTest.Parent.FullName, Is.EqualTo(@"c:\path"));
		}

		[Test]
		public void Parent_When_No_Parent_Should_Work()
		{
			mUnderTest = new FileImpl(@"c:\");
			Assert.Throws<DirectoryNotFoundException>(() => mUnderTest.Parent.ToString());
		}

		[Test]
		public void Name_Should_Work()
		{
			mUnderTest = new FileImpl(@"c:\path\file.txt");
			Assert.That(mUnderTest.Name, Is.EqualTo(@"file.txt"));
		}
		[Test]
		public void OpenRead_When_Exists_Should_Work()
		{
			File.AppendAllText(mTempFile, "test");
			using (var fileStream = mUnderTest.OpenRead())
			{
				var textReader = new StreamReader(fileStream);
				Assert.That(textReader.ReadToEnd(), Is.EqualTo("test"));
			}
		}

		[Test]
		public void OpenRead_When_NotExists_Should_Throw()
		{
			Assert.Throws<FileNotFoundException>(() => mUnderTest.OpenRead());
		}

		[Test]
		public void OpenWrite_When_Exists_Should_WriteOver()
		{
			File.WriteAllText(mTempFile, "some text");

			var fileStream = mUnderTest.OpenWrite();
			using (var textWriter = new StreamWriter(fileStream))
				textWriter.Write("write");

			Assert.That(File.ReadAllText(mTempFile), Is.EqualTo("writetext"));
		}

		[Test]
		public void OpenWrite_When_NotExists_Should_CreateFile()
		{
			var fileStream = mUnderTest.OpenWrite();
			using (var textWriter = new StreamWriter(fileStream)) textWriter.Write("write");

			Assert.That(File.ReadAllText(mTempFile), Is.EqualTo("write"));
		}

		[Test]
		public void OpenText_When_Exists_Should_Work()
		{
			File.AppendAllText(mTempFile, "test");
			using (var fileStream = mUnderTest.OpenText())
				Assert.That(fileStream.ReadToEnd(), Is.EqualTo("test"));
		}

		[Test]
		public void OpenText_When_NotExists_Should_Throw()
		{
			Assert.Throws<FileNotFoundException>(() => mUnderTest.OpenText());
		}

		[Test]
		public void AppendText_When_Exists_Should_Work()
		{
			File.WriteAllText(mTempFile, "some text");

			using (var fileStream = mUnderTest.AppendText())
				fileStream.Write("write");

			Assert.That(File.ReadAllText(mTempFile), Is.EqualTo("some textwrite"));
		}

		[Test]
		public void AppendText_When_NotExists_Should_CreateFile()
		{
			using (var fileStream = mUnderTest.AppendText())
				fileStream.Write("write");

			Assert.That(File.ReadAllText(mTempFile), Is.EqualTo("write"));
		}

		[Test]
		public void FileExists_Should_Work()
		{
			Assert.That(mUnderTest.Exists, Is.False);
			File.WriteAllText(mTempFile, "some text");
			Assert.That(mUnderTest.Exists, Is.True);
		}

		[Test]
		public void Delete_When_File_Not_Exist_Should_Do_Nothing()
		{
			mUnderTest.Delete();
		}

		[Test]
		public void Delete_Should_Work()
		{
			File.WriteAllText(mTempFile, "some text");
			mUnderTest.Delete();
			Assert.That(!File.Exists(mTempFile));
		}

		[Test]
		public void FileReplace_Should_Work()
		{
			File.WriteAllText(mTempFile, "blah");
			var destination = Path.GetTempFileName();

			mUnderTest.Replace(destination);

			Assert.That(File.Exists(mTempFile), Is.False);
			Assert.That(File.ReadAllText(destination), Is.EqualTo("blah"));
		}

		[Test]
		public void FileReplace_When_Destination_Does_NotExist_Should_Throw()
		{
			var destination = Path.GetTempFileName();
			File.Delete(destination);

			Assert.Throws<FileNotFoundException>(() => mUnderTest.Replace(mTempFile, destination));
		}

		[Test]
		public void FileCopyOrReplace_Should_Work()
		{
			File.WriteAllText(mTempFile, "blah");
			var destination = Path.GetTempFileName();

			mUnderTest.CopyOrReplace(destination);
			Assert.That(File.Exists(mTempFile), Is.False);
			Assert.That(File.ReadAllText(destination), Is.EqualTo("blah"));
		}

		[Test]
		public void FileCopyOrReplace_When_Destination_Does_NotExist_Should_Work()
		{
			File.WriteAllText(mTempFile, "blah");
			var destination = Path.GetTempFileName();
			File.Delete(destination);

			mUnderTest.CopyOrReplace(destination);
			Assert.That(File.Exists(mTempFile), Is.False);
			Assert.That(File.ReadAllText(destination), Is.EqualTo("blah"));
		}

		[Test]
		public void FileMove_Should_Work()
		{
			File.WriteAllText(mTempFile, "blah");
			var destination = Path.GetTempFileName();
			File.Delete(destination);

			Assert.That(File.Exists(mTempFile), Is.True);
			Assert.That(File.Exists(destination), Is.False);

			mUnderTest.MoveTo(destination);

			Assert.That(File.Exists(mTempFile), Is.False);
			Assert.That(File.Exists(destination), Is.True);
		}

		[Test]
		public void CopyOrReplace_When_No_Files_Exist_Should_Throw_And_Not_Create_Any_Files()
		{
			var source = Path.GetTempFileName();
			File.Delete(source);
			var destination = Path.GetTempFileName();
			File.Delete(destination);

			Assert.That(File.Exists(source), Is.False);
			Assert.That(File.Exists(destination), Is.False);

			var underTest = new FileImpl(source);
			Assert.Throws<FileNotFoundException>(() => underTest.CopyOrReplace(destination));

			Assert.That(File.Exists(source), Is.False);
			Assert.That(File.Exists(destination), Is.False);
		}

		[Test]
		public void RelativeTo_Should_Work()
		{
			new FileImpl(@"A\B\C\D\file.txt").RelativeTo(new DirectoryImpl(@"A\B\C")).Should().Be(@"D/file.txt");
			new FileImpl(@"A\B\C\D\file.txt").RelativeTo(new DirectoryImpl(@"A\B\C\")).Should().Be(@"D/file.txt");
			new FileImpl(@"A\B\C\D\file.txt").RelativeTo(new DirectoryImpl(@"A/B/C/")).Should().Be(@"D/file.txt");
			new FileImpl(@"A\B\C/D\file.txt").RelativeTo(new DirectoryImpl(@"A\B\C")).Should().Be(@"D/file.txt");
		}
		[Test]
		public void RelativeTo_When_No_Common_Root_Should_Work()
		{
			Assert.Throws<ArgumentException>(() => new FileImpl(@"One\file.txt").RelativeTo(new DirectoryImpl(@"Another")));
		}

		[Test]
		public void ChangeExtension_When_Condition_Should_Work()
		{
			mUnderTest = new FileImpl("folder/file.txt");
			mUnderTest.ChangeExtension(".xml").FullName.Should().Be("folder/file.xml");
		}
	}
}