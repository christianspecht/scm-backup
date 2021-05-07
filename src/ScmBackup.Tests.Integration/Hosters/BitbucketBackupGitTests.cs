using ScmBackup.Configuration;
using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;
using ScmBackup.Scm;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class BitbucketBackupGitTests : IBackupTests
    {
        private string prefix = "Bitbucket";

        internal override string PublicUserName { get { return "scm-backup-testuser"; } }
        internal override string PublicRepoName { get { return "scm-backup-test-git"; } }

        internal override string PrivateUserName { get { return TestHelper.EnvVar(prefix, "Name"); } }
        internal override string PrivateRepoName { get { return TestHelper.EnvVar(prefix, "RepoPrivateGit"); } }

        protected override void Setup(bool usePrivateRepo)
        {
            // re-use test repo for Api tests
            this.source = new ConfigSource();
            this.source.Hoster = "bitbucket";
            this.source.Type = "user";
            this.source.Name = this.GetUserName(usePrivateRepo);
            this.source.AuthName = TestHelper.EnvVar(prefix, "Name");
            this.source.Password = TestHelper.EnvVar(prefix, "PW");

            var config = new Config();
            config.Sources.Add(this.source);

            var context = new FakeContext();
            context.Config = config;

            var api = new BitbucketApi(new HttpRequest());
            var repoList = api.GetRepositoryList(this.source);
            this.repo = repoList.Find(r => r.ShortName == this.GetRepoName(usePrivateRepo));

            this.scm = new GitScm(new FileSystemHelper(), context);
            Assert.True(this.scm.IsOnThisComputer());

            var scmFactory = new FakeScmFactory();
            scmFactory.Register(ScmType.Git, this.scm);
            this.sut = new BitbucketBackup(scmFactory);
        }

        protected override void AssertRepo(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, "389dae62982075f97efb660824c31f712872a9cd"));
        }

        protected override void AssertWiki(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, "8c621fd488ee5fa1ed19ca78113ccc92d55820bd"));
        }

        protected override void AssertPrivateRepo(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
        }
    }
}
