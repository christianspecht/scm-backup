using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class BitbucketApiTests : IHosterApiTests
    {
        //  user, repo etc.
        internal override string HosterUser { get { return "scm-backup-testuser"; } }
        internal override string HosterOrganization { get { return "scm-backup-testteam"; } }
        internal override string HosterRepo { get { return "scm-backup-test"; } }
        internal override string HosterCommit { get { return "617f9e55262be7b6d1c9db081ec351ff25c9a0e5"; } }
        internal override string HosterWikiCommit { get { return "befce8ddfb6976918c3c3e1a44fb6a68a438b785"; } }
        internal override string HosterPaginationUser { get { return "evzijst"; } }
        internal override string HosterPrivateRepo { get { return TestHelper.EnvVar(this.EnvVarPrefix, "RepoPrivateGit"); } }

        internal override string EnvVarPrefix
        {
            get { return "Bitbucket"; }
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
    }
}
