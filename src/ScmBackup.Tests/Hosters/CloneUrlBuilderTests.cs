using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class CloneUrlBuilderTests
    {
        [Fact]
        public void BuildsGithubCloneUrl()
        {
            var result = CloneUrlBuilder.GithubCloneUrl("foo", "bar");

            Assert.Equal("https://github.com/foo/bar", result);
        }

        [Fact]
        public void BuildsBitbucketCloneUrl()
        {
            var result = CloneUrlBuilder.BitbucketCloneUrl("foo", "bar");

            Assert.Equal("https://bitbucket.org/foo/bar", result);
        }
    }
}
