using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class BitbucketApiTests : IHosterApiTests
    {
        internal override string EnvVarPrefix
        {
            get { return "BitbucketApiTests"; }
        }

        internal override string ConfigHoster
        {
            get { return "bitbucket"; }
        }

        internal override int Pagination_MinNumberOfRepos
        {
            get { return 11; } // https://developer.atlassian.com/bitbucket/api/2/reference/meta/pagination
        }

        public BitbucketApiTests()
        {
            this.sut = new BitbucketApi(new HttpRequest());
        }
    }
}
