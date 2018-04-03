using ScmBackup.Hosters.Github;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class GithubApiTests : IHosterApiTests
    {
        internal override string EnvVarPrefix
        {
            get { return "GithubApiTests"; }
        }

        internal override string ConfigHoster
        {
            get { return "github"; }
        }

        internal override int Pagination_MinNumberOfRepos
        {
            get { return 101; } // https://developer.github.com/v3/#pagination
        }

        public GithubApiTests()
        {
            this.sut = new GithubApi(new FakeContext());
        }
    }
}
