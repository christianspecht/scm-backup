using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;
using ScmBackup.Scm;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class BitbucketBackupMercurialTests : IBackupTests
    {
        private string prefix = "Bitbucket";

        internal override string PublicRepoName
        {
            get { return TestHelper.EnvVar(prefix, "Repo"); }
        }

        internal override string PrivateRepoName
        {
            get { return TestHelper.EnvVar(prefix, "RepoPrivate"); }
        }

        protected override void Setup(string repoName)
        {
            // re-use test repo for Api tests
            this.source = new ConfigSource();
            this.source.Hoster = "bitbucket";
            this.source.Type = "user";
            this.source.Name = TestHelper.EnvVar(prefix, "Name");
            this.source.AuthName = this.source.Name;
            this.source.Password = TestHelper.EnvVar(prefix, "PW");

            var config = new Config();
            config.Sources.Add(this.source);
            
            var context = new FakeContext();
            context.Config = config;

            var api = new BitbucketApi(new HttpRequest());
            var repoList = api.GetRepositoryList(this.source);
            this.repo = repoList.Find(r => r.ShortName == repoName);
            
            this.scm = new MercurialScm(new FileSystemHelper(), context);
            Assert.True(this.scm.IsOnThisComputer());

            var scmFactory = new FakeScmFactory();
            scmFactory.Register(ScmType.Mercurial, this.scm);
            this.sut = new BitbucketBackup(scmFactory);
        }

        protected override void AssertRepo(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, TestHelper.EnvVar(prefix, "Commit")));
        }

        protected override void AssertWiki(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, TestHelper.EnvVar(prefix, "WikiCommit")));
        }

        protected override void AssertPrivateRepo(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
        }
    }
}
