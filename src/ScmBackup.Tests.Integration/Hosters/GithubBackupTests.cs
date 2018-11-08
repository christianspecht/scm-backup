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

        internal override string PublicUserName { get { return "scm-backup-testuser"; } }
        internal override string PublicRepoName { get { return "scm-backup"; } }

        protected override void Setup(bool usePrivateRepo)
        {
            // re-use test repo for GithubApi tests
            this.source = new ConfigSource();
            this.source.Hoster = "github";
            this.source.Type = "user";
            this.source.Name = this.GetUserName(usePrivateRepo);
            this.source.AuthName = TestHelper.EnvVar("Github_Name");
            this.source.Password = TestHelper.EnvVar("Github_PW");

            var config = new Config();
            config.Sources.Add(this.source);

            var context = new FakeContext();
            context.Config = config;

            var factory = new FakeScmFactory();
            factory.Register(ScmType.Git, new GitScm(new FileSystemHelper(), context));

            var api = new GithubApi(context, factory);
            this.repoList = api.GetRepositoryList(this.source);
            this.repo = this.repoList.Find(r => r.ShortName == this.GetRepoName(usePrivateRepo));
            
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
            Assert.True(scm.RepositoryContainsCommit(dir, "7be29139f4cdc4037647fc2f21d9d82c42a96e88"));
        }

        protected override void AssertWiki(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, "714ddb8c48cebc70ff2ae74be98ac7cdf91ade6e"));
        }
    }
}
