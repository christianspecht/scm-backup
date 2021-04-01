using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class BitbucketApiTests : IHosterApiTests
    {
        //  user, repo etc.
        internal override string HosterUser { get { return "scm-backup-testuser"; } }
        internal override string HosterOrganization { get { return "scm-backup-testteam"; } }
        internal override string HosterRepo { get { return "scm-backup-test-git"; } }
        internal override string HosterCommit { get { return "389dae62982075f97efb660824c31f712872a9cd"; } }
        internal override string HosterWikiCommit { get { return "8c621fd488ee5fa1ed19ca78113ccc92d55820bd"; } }
        internal override string HosterPaginationUser { get { return "evzijst"; } }
        internal override string HosterPrivateRepo { get { return TestHelper.EnvVar(this.EnvVarPrefix, "RepoPrivateGit"); } }

        internal override string EnvVarPrefix
        {
            get { return "Tests_Bitbucket"; }
        }

        internal override string ConfigHoster
        {
            get { return "bitbucket"; }
        }

        internal override int Pagination_MinNumberOfRepos
        {
            get { return 11; } // https://developer.atlassian.com/bitbucket/api/2/reference/meta/pagination
        }

        internal override bool SkipUnauthenticatedTests
        {
            get { return false; }
        }

        public BitbucketApiTests()
        {
            this.sut = new BitbucketApi(new HttpRequest());
        }

        [Fact]
        public void GetRepositoryList_DoesntReturnMercurialRepos()
        {
            // #60: 2 months after Bitbucket's HG deprecation, their API still returns HG repos but cloning/pulling them fails -> ignore them
            var source = new ConfigSource();
            source.Hoster = this.ConfigHoster;
            source.Type = "user";
            source.Name = this.HosterUser;
            source.AuthName = TestHelper.EnvVar(this.EnvVarPrefix, "Name");
            source.Password = TestHelper.EnvVar(this.EnvVarPrefix, "PW");

            var repoList = sut.GetRepositoryList(source);

            var hgrepos = repoList.Where(x => x.Scm == ScmType.Mercurial);
            Assert.Empty(hgrepos);
        }
    }
}
