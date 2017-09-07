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

        // public and private test repositories
        internal abstract string PublicRepoUrl { get; }
        internal abstract string PrivateRepoUrl { get; }

        // commit ids that do/do not exist in the public repo
        internal abstract string PublicRepoExistingCommitId { get; }
        internal abstract string PublicRepoNonExistingCommitId{get;}

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

        [Fact]
        public void CreateRepositoryDoesNothingWhenDirectoryIsARepository()
        {
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("create-2"));

            sut.IsOnThisComputer(this.config);
            sut.CreateRepository(dir);

            // this should do nothing
            sut.CreateRepository(dir);

            Assert.True(sut.DirectoryIsRepository(dir));
        }

        [Fact]
        public void CreateRepositoryCreatesNonExistingDirectory()
        {
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("create-3"));
            string subDir = Path.Combine(dir, "sub");

            sut.IsOnThisComputer(this.config);
            sut.CreateRepository(subDir);

            Assert.True(Directory.Exists(subDir));
        }

        [Fact]
        public void PullFromRemote_PublicUrl_CreatesNewRepo()
        {
            sut.IsOnThisComputer(this.config);
            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("pull-new"));
            string subDir = Path.Combine(dir, "sub");

            sut.PullFromRemote(this.PublicRepoUrl, subDir);

            Assert.True(Directory.Exists(subDir));
            Assert.True(sut.DirectoryIsRepository(subDir));
        }

        [Fact]
        public void PullFromRemote_PublicUrl_UpdatesExistingRepo()
        {
            sut.IsOnThisComputer(this.config);

            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("pull-existing"));
            sut.CreateRepository(dir);

            sut.PullFromRemote(this.PublicRepoUrl, dir);

            Assert.True(sut.DirectoryIsRepository(dir));
            // TODO: find out whether the local repo contains commits from the remote repo
        }

        [Fact]
        public void PullFromRemote_PublicUrl_ThrowsWhenDirIsNotEmpty()
        {
            sut.IsOnThisComputer(this.config);

            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("pull-not-empty"));
            File.WriteAllText(Path.Combine(dir, "foo.txt"), "foo");

            Assert.Throws<InvalidOperationException>(() => sut.PullFromRemote(this.PublicRepoUrl, dir)); 
        }

        [Fact]
        public void RepositoryContainsCommit_ThrowsWhenDirDoesntExist()
        {
            sut.IsOnThisComputer(this.config);

            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("contains-nodir"));
            string subDir = Path.Combine(dir, "sub");

            Assert.Throws<DirectoryNotFoundException>(() => sut.RepositoryContainsCommit(subDir, "foo"));
        }

        [Fact]
        public void RepositoryContainsCommit_ThrowsWhenDirIsNoRepo()
        {
            sut.IsOnThisComputer(this.config);

            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("contains-norepo"));

            Assert.Throws<InvalidOperationException>(() => sut.RepositoryContainsCommit(dir, "foo"));
        }

        [Fact]
        public void RepositoryContainsCommit_ReturnsTrueWhenCommitExists()
        {
            sut.IsOnThisComputer(this.config);

            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("contains-commit"));

            sut.PullFromRemote(this.PublicRepoUrl, dir);

            Assert.True(sut.RepositoryContainsCommit(dir, this.PublicRepoExistingCommitId));
        }

        [Fact]
        public void RepositoryContainsCommit_ReturnsFalseWhenCommitDoesntExist()
        {
            sut.IsOnThisComputer(this.config);

            string dir = DirectoryHelper.CreateTempDirectory(DirSuffix("contains-nocommit"));

            sut.PullFromRemote(this.PublicRepoUrl, dir);

            Assert.False(sut.RepositoryContainsCommit(dir, this.PublicRepoNonExistingCommitId));
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
