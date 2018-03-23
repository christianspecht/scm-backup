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

        public GithubApiTests()
        {
            this.sut = new GithubApi(new FakeContext());
        }
    }
}
