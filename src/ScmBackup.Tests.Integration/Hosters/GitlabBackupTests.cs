using ScmBackup.Configuration;
using ScmBackup.Hosters.Gitlab;
using ScmBackup.Http;
using ScmBackup.Scm;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class GitlabBackupTests : IBackupTests
    {
        private string prefix = "Tests_Gitlab";

        internal override string PublicUserName { get { return "scm-backup-testuser"; } }
        internal override string PublicRepoName { get { return "scm-backup-test"; } }
        internal override string PrivateUserName { get { return TestHelper.EnvVar(prefix, "Name"); } }
        internal override string PrivateRepoName { get { return TestHelper.EnvVar(prefix, "RepoPrivate"); } }

        protected override void Setup(bool usePrivateRepo)
        {
            // re-use test repo for Api tests
            var s = new ConfigSource();
            s.Hoster = "gitlab";
            s.Type = "user";
            s.Name = this.GetUserName(usePrivateRepo);
            s.AuthName = TestHelper.EnvVar(prefix, "Name");
            s.Password= TestHelper.EnvVar(prefix, "PW");
            this.source = s;

            var config = new Config();
            config.Sources.Add(s);

            var context = new FakeContext();
            context.Config = config;

            var factory = new FakeScmFactory();
            factory.Register(ScmType.Git, new GitScm(new FileSystemHelper(), context));

            var api = new GitlabApi(new HttpRequest(), factory);
            var repoList = api.GetRepositoryList(this.source);
            this.repo = repoList.Find(r=>r.ShortName == this.GetRepoName(usePrivateRepo));

            this.scm = new GitScm(new FileSystemHelper(), context);
            Assert.True(this.scm.IsOnThisComputer());

            var scmfactory = new FakeScmFactory();
            scmfactory.Register(ScmType.Git, this.scm);
            this.sut = new GitlabBackup(scmfactory);
        }

        protected override void AssertRepo(string dir)
        {
            this.DefaultRepoAssert(dir, "d7c9ad8185b7707dbcc907e41154e3e5e5b2a540");
        }

        protected override void AssertPrivateRepo(string dir)
        {
            this.DefaultRepoAssert(dir);
        }

        protected override void AssertWiki(string dir)
        {
            this.DefaultRepoAssert(dir, "5893873f9da26fc59bbeaafde5fad5800907e56f");
        }
    }
}
