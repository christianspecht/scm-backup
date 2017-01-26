using ScmBackup.Scm;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Scm
{
    public class GitScmTests
    {
        public GitScmTests()
        {
            this.config = new Config();
        }

        private readonly Config config;

        [Fact]
        public void IsOnThisComputerExecutes()
        {
            var sut = new GitScm();
            sut.IsOnThisComputer(this.config);
        }

        [Fact]
        public void IsOnThisComputerReturnsTrue()
        {
            // Some of the other integration tests will need to use Git -> make sure that the machine running the tests has Git installed
            var sut = new GitScm();
            var result = sut.IsOnThisComputer(this.config);

            Assert.True(result);
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForEmptyDir()
        {
            string dir = TempDirectoryHelper.CreateTempDirectory();

            var sut = new GitScm();
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(dir));
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForNonEmptyDir()
        {
            string dir = TempDirectoryHelper.CreateTempDirectory();
            string subDir = Path.Combine(dir, "sub");
            Directory.CreateDirectory(subDir);
            File.WriteAllText(Path.Combine(dir, "foo.txt"), "foo");

            var sut = new GitScm();
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(dir));
        }

        [Fact]
        public void CreateRepositoryCreatesNewRepository()
        {
            string dir = TempDirectoryHelper.CreateTempDirectory();

            var sut = new GitScm();
            sut.IsOnThisComputer(this.config);
            sut.CreateRepository(dir);

            Assert.True(sut.DirectoryIsRepository(dir));
        }
    }
}
