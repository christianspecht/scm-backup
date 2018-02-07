using ScmBackup.Hosters;
using ScmBackup.Scm;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public abstract class IBackupTests
    {
        internal BackupBase sut;
        internal HosterRepository repo;
        internal IScm scm;

        // The child classes need to implement this *and fill all above properties there*:
        protected abstract void Setup();

        // The child classes need to implement this (Wiki/Issues only if necessary)
        protected abstract void AssertRepo(string dir);
        protected virtual void AssertWiki(string dir) { }
        protected virtual void AssertIssues(string dir) { }


        [Fact]
        public void MakesBackup()
        {
            var dir = DirectoryHelper.CreateTempDirectory(this.DirSuffix("makes-backup"));

            this.Setup();

            // these should have been filled by the child classes' Setup() method
            Assert.NotNull(this.sut);
            Assert.NotNull(this.repo);
            Assert.NotNull(this.scm);

            sut.MakeBackup(this.repo, dir);

            this.AssertRepo(Path.Combine(dir, this.sut.SubDirRepo));
            this.AssertWiki(Path.Combine(dir, this.sut.SubDirWiki));
            this.AssertIssues(Path.Combine(dir, this.sut.SubDirIssues));
        }

        /// <summary>
        /// helper for directory suffixes
        /// </summary>
        private string DirSuffix(string suffix)
        {
            return "ibackup-" + this.GetType().Name.ToLower() + "-" + suffix;
        }
    }
}
