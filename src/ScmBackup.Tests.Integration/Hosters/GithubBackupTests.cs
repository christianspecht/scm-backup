using ScmBackup.Hosters.Github;
using ScmBackup.Http;
using ScmBackup.Scm;
using System.IO;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class GithubBackupTests : IBackupTests
    {
        protected override void Setup()
        {
            // re-use test repo for GithubApi tests
            var source = new ConfigSource();
            source.Hoster = "github";
            source.Type = "user";
            source.Name = TestHelper.EnvVar("GithubApiTests_Name");
            source.AuthName = source.Name;
            source.Password = TestHelper.EnvVar("GithubApiTests_PW");

            this.config = new Config();
            this.config.Sources.Add(source);

            var context = new FakeContext();
            context.Config = this.config;

            var api = new GithubApi(new HttpRequest(), new FakeLogger());
            this.repo = api.GetRepositoryList(source).First();
            
            this.scm = new GitScm(new FileSystemHelper(), context);
            Assert.True(this.scm.IsOnThisComputer(this.config));

            var scmFactory = new FakeScmFactory();
            scmFactory.Register(ScmType.Git, this.scm);
            this.sut = new GithubBackup(scmFactory);
        }

        protected override void AssertRepo(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, TestHelper.EnvVar("GithubApiTests_Commit")));
        }

        protected override void AssertWiki(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, TestHelper.EnvVar("GithubApiTests_WikiCommit")));
        }
    }
}
