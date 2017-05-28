using ScmBackup.Scm;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Scm
{
    public abstract class IScmTests
    {
        internal Config config;
        internal IScm sut;

        public IScmTests()
        {
            this.config = new Config();
        }

        [Fact]
        public void SutWasSetInChildClass()
        {
            Assert.NotNull(this.sut);
        }

        [Fact]
        public void IsOnThisComputerExecutes()
        {
            sut.IsOnThisComputer(this.config);
        }

        [Fact]
        public void IsOnThisComputerReturnsTrue()
        {
            // Some of the other integration tests will need to use the SCM -> make sure that it's installed on the machine running the tests
            var result = sut.IsOnThisComputer(this.config);

            Assert.True(result);
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForNonExistingDir()
        {
            string dir = TempDirectoryHelper.CreateTempDirectory();
            string subDir = Path.Combine(dir, "sub");
            
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(subDir));
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForEmptyDir()
        {
            string dir = TempDirectoryHelper.CreateTempDirectory();
            
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
            
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(dir));
        }

        [Fact]
        public void CreateRepositoryCreatesNewRepository()
        {
            string dir = TempDirectoryHelper.CreateTempDirectory();
            
            sut.IsOnThisComputer(this.config);
            sut.CreateRepository(dir);

            Assert.True(sut.DirectoryIsRepository(dir));
        }
    }
}
