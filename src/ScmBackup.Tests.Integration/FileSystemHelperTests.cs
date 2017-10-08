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

            var sut = new FileSystemHelper();
            Assert.True(sut.DirectoryIsEmpty(dir));
        }

        [Fact]
        public void DirectoryIsEmpty_ReturnsFalseWhenDirContainsFile()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-2");
            File.WriteAllText(Path.Combine(dir, "foo.txt"), "foo");

            var sut = new FileSystemHelper();
            Assert.False(sut.DirectoryIsEmpty(dir));
        }

        [Fact]
        public void DirectoryIsEmpty_ReturnsFalseWhenDirContainsSubdir()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-3");
            Directory.CreateDirectory(Path.Combine(dir, "subdir"));

            var sut = new FileSystemHelper();
            Assert.False(sut.DirectoryIsEmpty(dir));
        }

        [Fact]
        public void DirectoryIsEmpty_ThrowsWhenDirDoesntExist()
        {
            string dir = DirectoryHelper.CreateTempDirectory("fsh-4");
            string subDir = Path.Combine(dir, "doesnt-exist");

            var sut = new FileSystemHelper();
            Assert.Throws<DirectoryNotFoundException>(() => sut.DirectoryIsEmpty(subDir));
        }

        [Fact]
        public void DirectoryIsEmpty_ThrowsWhenDirIsMissing()
        {
            var sut = new FileSystemHelper();
            Assert.Throws<ArgumentNullException>(() => sut.DirectoryIsEmpty(null));
            Assert.Throws<ArgumentException>(() => sut.DirectoryIsEmpty(string.Empty));
        }
    }
}
