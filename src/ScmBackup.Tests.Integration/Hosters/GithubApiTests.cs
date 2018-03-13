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
            var logger = new TestLogger(this.EnvVarPrefix);
            var request = HttpLogHelper.GetRequest(logger);

            this.sut = new GithubApi(request, logger);
        }
    }
}
