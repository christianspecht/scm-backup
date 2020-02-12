using ScmBackup.Configuration;
using ScmBackup.Hosters.Github;
using ScmBackup.Scm;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class GithubApiTests : IHosterApiTests
    {
        //  user, repo etc.
        internal override string HosterUser { get { return "scm-backup-testuser"; } }
        internal override string HosterOrganization { get { return "scm-backup-testorg"; } }
        internal override string HosterRepo { get { return "scm-backup"; } }
        internal override string HosterCommit { get { return "7be29139f4cdc4037647fc2f21d9d82c42a96e88"; } }
        internal override string HosterWikiCommit { get { return "714ddb8c48cebc70ff2ae74be98ac7cdf91ade6e"; } }
        internal override string HosterPaginationUser { get { return "shanselman"; } }
        internal override string HosterPrivateRepo { get { return TestHelper.EnvVar(this.EnvVarPrefix, "RepoPrivate"); } }

        internal override string EnvVarPrefix
        {
            get { return "Github"; }
        }

        internal override string ConfigHoster
        {
            get { return "github"; }
        }

        internal override int Pagination_MinNumberOfRepos
        {
            get { return 101; } // https://developer.github.com/v3/#pagination
        }

        internal override bool SkipUnauthenticatedTests
        {
            // those sometimes fail on AppVeyor because of rate limits, see #7
            get { return TestHelper.RunsOnAppVeyor(); }
        }

        protected override bool SkipTestsIssue15()
        {
            return TestHelper.RunsOnAppVeyor();
        }

        public GithubApiTests()
        {
            var context = new FakeContext();
            var factory = new FakeScmFactory();
            factory.Register(ScmType.Git, new GitScm(new FileSystemHelper(), context));

            this.sut = new GithubApi(context, factory);
        }

        [SkippableFact]
        public void SetsWikiToFalseWhenWikiDoesntExist()
        {
            Skip.If(this.SkipTestsIssue15());

            // issue #13: the GitHub API only returns whether it's *possible* to create a wiki, but not if the repo actually *has* a wiki.

            // This is a test repo without wiki, but with the "wiki" checkbox set:
            string username = "scm-backup-testuser";
            string reponame = "wiki-doesnt-exist";

            // We always use this repo, but authenticate with the user from the config to avoid hitting rate limits:
            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = username;
            source.AuthName = TestHelper.EnvVar(this.EnvVarPrefix, "Name");
            source.Password = TestHelper.EnvVar(this.EnvVarPrefix, "PW");

            var repoList = sut.GetRepositoryList(source);
            var repo = repoList.First(r => r.ShortName == reponame);

            Assert.False(repo.HasWiki);
        }
    }
}
