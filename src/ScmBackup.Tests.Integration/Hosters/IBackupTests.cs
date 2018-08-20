using ScmBackup.Hosters;
using ScmBackup.Scm;
using System;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public abstract class IBackupTests
    {
        internal BackupBase sut;
        internal HosterRepository repo;
        internal IScm scm;
        internal ConfigSource source;

        // The child classes need to implement this *and fill all above properties there*:
        protected abstract void Setup(string repoName);

        // The child classes need to implement this (Wiki/Issues only if necessary)
        internal abstract string PublicRepoName { get; }
        protected abstract void AssertRepo(string dir);
        protected virtual void AssertWiki(string dir) { }
        protected virtual void AssertIssues(string dir) { }

        // The child classes need to implement this, IF this hoster has private repos:
        internal virtual string PrivateRepoName { get { return null; } }
        protected virtual void AssertPrivateRepo(string dir) { }


        [Fact]
        public void MakesBackup()
        {
            Assert.NotNull(this.PublicRepoName); // Note: PrivateRepoName is optional

            var dir = DirectoryHelper.CreateTempDirectory(this.DirSuffix("makes-backup"));

            this.Setup(this.PublicRepoName);

            // these should have been filled by the child classes' Setup() method
            Assert.NotNull(this.sut);
            Assert.NotNull(this.repo);
            Assert.NotNull(this.scm);
            Assert.NotNull(this.source);

            sut.MakeBackup(this.source, this.repo, dir);

            this.AssertRepo(Path.Combine(dir, this.sut.SubDirRepo));
            this.AssertWiki(Path.Combine(dir, this.sut.SubDirWiki));
            this.AssertIssues(Path.Combine(dir, this.sut.SubDirIssues));
        }

        [SkippableFact]
        public void MakesBackupOfPrivateRepo()
        {
            Skip.If(this.PrivateRepoName == null, "There's no private repo for this hoster");

            var dir = DirectoryHelper.CreateTempDirectory(this.DirSuffix("makes-backup-private"));

            this.Setup(this.PrivateRepoName);

            sut.MakeBackup(this.source, this.repo, dir);

            this.AssertPrivateRepo(Path.Combine(dir, sut.SubDirRepo));
        }

        [Fact]
        public void DoesntBackupWikiIfNotSet()
        {
            var dir = DirectoryHelper.CreateTempDirectory(this.DirSuffix("doesnt-backup-wiki"));
            this.Setup(this.PublicRepoName);

            this.repo.SetWiki(false, null);

            sut.MakeBackup(this.source, this.repo, dir);

            Assert.False(Directory.Exists(sut.SubDirWiki));
        }

        [Fact]
        public void DoesntBackupIssuesIfNotSet()
        {
            var dir = DirectoryHelper.CreateTempDirectory(this.DirSuffix("doesnt-backup-issues"));
            this.Setup(this.PublicRepoName);

            this.repo.SetIssues(false, null);

            sut.MakeBackup(this.source, this.repo, dir);

            Assert.False(Directory.Exists(sut.SubDirIssues));
        }

        [Fact]
        public void ThrowsWhenScmFactoryIsNull()
        {
            var dir = DirectoryHelper.CreateTempDirectory(this.DirSuffix("throws-when-scmfactory-null"));
            this.Setup(this.PublicRepoName);
            sut.scmFactory = null;

            Assert.Throws<ArgumentNullException>(() => sut.MakeBackup(this.source, this.repo, dir));
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
