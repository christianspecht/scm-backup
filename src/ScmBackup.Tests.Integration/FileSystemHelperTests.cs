using System.IO;
using System;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class FileSystemHelperTests
    {
        [Fact]
        public void DirectoryIsEmpty_ReturnsTrueWhenEmpty()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-1");

            Assert.True(FileSystemHelper.DirectoryIsEmpty(dir));
        }

        [Fact]
        public void DirectoryIsEmpty_ReturnsFalseWhenDirContainsFile()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-2");
            File.WriteAllText(Path.Combine(dir, "foo.txt"), "foo");

            Assert.False(FileSystemHelper.DirectoryIsEmpty(dir));
        }

        [Fact]
        public void DirectoryIsEmpty_ReturnsFalseWhenDirContainsSubdir()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-3");
            Directory.CreateDirectory(Path.Combine(dir, "subdir"));

            Assert.False(FileSystemHelper.DirectoryIsEmpty(dir));
        }

        [Fact]
        public void DirectoryIsEmpty_ThrowsWhenDirDoesntExist()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-4");
            string subDir = Path.Combine(dir, "doesnt-exist");

            Assert.Throws<DirectoryNotFoundException>(() => FileSystemHelper.DirectoryIsEmpty(subDir));
        }

        [Fact]
        public void DirectoryIsEmpty_ThrowsWhenDirIsMissing()
        {
            Assert.Throws<ArgumentNullException>(() => FileSystemHelper.DirectoryIsEmpty(null));
            Assert.Throws<ArgumentException>(() => FileSystemHelper.DirectoryIsEmpty(string.Empty));
        }
    }
}
