using RichardSzalay.MockHttp;
using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;
using System.Net;
using System.Net.Http;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class BitbucketWikiRemovalTests
    {
        [Fact]
        public void GetRepositoryList_IgnoresLegacyWikiFlagButKeepsGitRepositories()
        {
            // Legacy Bitbucket API responses may still say has_wiki=true after wiki removal.
            const string json = @"{
                ""values"": [
                    {
                        ""scm"": ""git"",
                        ""slug"": ""with-wiki"",
                        ""full_name"": ""workspace/with-wiki"",
                        ""has_wiki"": true,
                        ""is_private"": true,
                        ""links"": { ""clone"": [ { ""name"": ""https"", ""href"": ""https://bitbucket.org/workspace/with-wiki.git"" } ] }
                    },
                    {
                        ""scm"": ""git"",
                        ""slug"": ""without-wiki"",
                        ""full_name"": ""workspace/without-wiki"",
                        ""has_wiki"": false,
                        ""is_private"": false,
                        ""links"": { ""clone"": [ { ""name"": ""https"", ""href"": ""https://bitbucket.org/workspace/without-wiki.git"" } ] }
                    }
                ],
                ""next"": null
            }";

            var mock = new MockHttpMessageHandler();
            mock.Expect("https://api.bitbucket.org/2.0/repositories/workspace")
                .Respond(HttpStatusCode.OK, "application/json", json);

            var request = new HttpRequest { HttpClient = new HttpClient(mock) };
            var api = new BitbucketApi(request);
            var source = new ConfigSource { Hoster = "bitbucket", Type = "user", Name = "workspace" };

            var repos = api.GetRepositoryList(source);

            Assert.Equal(2, repos.Count);
            var repo = repos.Single(x => x.ShortName == "with-wiki");
            Assert.Equal(ScmType.Git, repo.Scm);
            Assert.Equal("https://bitbucket.org/workspace/with-wiki.git", repo.CloneUrl);
            Assert.True(repo.IsPrivate);
            Assert.False(repo.HasWiki);
            Assert.Null(repo.WikiUrl);
            Assert.All(repos, x => Assert.False(x.HasWiki));
            mock.VerifyNoOutstandingExpectation();
        }
    }
}
