using ScmBackup.Scm;
using System;
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
        public void GetVersionNumberReturnsVersionNumber()
        {
            sut.IsOnThisComputer(this.config);

            // Getting the SCM's version number without the method under test is difficult -> just check whether it executes and returns something
            var result = sut.GetVersionNumber();

            Assert.False(string.IsNullOrWhiteSpace(result));

            // output version number to be sure
            Console.WriteLine("{0} version {1}", sut.DisplayName, result);
        }

        [Fact]
        public void GetVersionNumberDoesntContainSpecialCharacters()
        {
            sut.IsOnThisComputer(this.config);
            var result = sut.GetVersionNumber();

            Assert.False(result.Contains("\r"), "contains \\r");
            Assert.False(result.Contains("\n"), "contains \\n");
            Assert.False(result.Contains("\t"), "contains \\t");
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForNonExistingDir()
        {
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("non-existing"));
            string subDir = Path.Combine(dir, "sub");
            
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(subDir));
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForEmptyDir()
        {
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("empty"));
            
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(dir));
        }

        [Fact]
        public void DirectoryIsRepositoryReturnsFalseForNonEmptyDir()
        {
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("non-empty"));
            string subDir = Path.Combine(dir, "sub");
            Directory.CreateDirectory(subDir);
            File.WriteAllText(Path.Combine(dir, "foo.txt"), "foo");
            
            sut.IsOnThisComputer(this.config);

            Assert.False(sut.DirectoryIsRepository(dir));
        }

        [Fact]
        public void CreateRepositoryCreatesNewRepository()
        {
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("create"));
            
            sut.IsOnThisComputer(this.config);
            sut.CreateRepository(dir);

            Assert.True(sut.DirectoryIsRepository(dir));
        }

        /// <summary>
        /// helper for directory suffixes
        /// </summary>
        private string DirSuffix(string suffix)
        {
            return "iscm-" + this.sut.ShortName + "-" + suffix;
        }
    }
}
