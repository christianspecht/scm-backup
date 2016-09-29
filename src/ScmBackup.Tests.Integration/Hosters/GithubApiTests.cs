using ScmBackup.Hosters;
using ScmBackup.Http;
using System.Net;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class GithubApiTests
    {
        [Fact]
        public void CallsGithubApi_UnauthenticatedUser()
        {
            var config = new ConfigSource();
            config.Hoster = "github";
            config.Type = "user";
            config.Name = "christianspecht";

            var logger = new FakeLogger();
            var request = new HttpRequest(logger);

            var sut = new GithubApi(request, logger);

            var repoList = sut.GetRepositoryList(config);

            // HTTP status ok?
            Assert.Equal(HttpStatusCode.OK, sut.LastResult.Status);

            // at least one result?
            Assert.NotNull(repoList);
            Assert.True(repoList.Count > 0);
        }
    }
}
