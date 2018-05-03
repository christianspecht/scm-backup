using ScmBackup.Hosters;
using ScmBackup.Hosters.Github;
using ScmBackup.Scm;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class GithubBackupTests : IBackupTests
    {
        private List<HosterRepository> repoList;

        protected override void Setup()
        {
            // re-use test repo for GithubApi tests
            var source = new ConfigSource();
            source.Hoster = "github";
            source.Type = "user";
            source.Name = TestHelper.EnvVar("GithubApiTests_Name");
            source.AuthName = source.Name;
            source.Password = TestHelper.EnvVar("GithubApiTests_PW");

            var config = new Config();
            config.Sources.Add(source);

            var context = new FakeContext();
            context.Config = config;

            var api = new GithubApi(context);
            this.repoList = api.GetRepositoryList(source);
            this.repo = this.repoList.Find(r => r.ShortName == TestHelper.EnvVar("GithubApiTests_Repo"));
            
            this.scm = new GitScm(new FileSystemHelper(), context);
            Assert.True(this.scm.IsOnThisComputer());

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
