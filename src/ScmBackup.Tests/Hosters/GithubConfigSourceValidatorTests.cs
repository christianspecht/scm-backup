using ScmBackup.Hosters;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class GithubConfigSourceValidatorTests
    {
        private ConfigSource config;

        public GithubConfigSourceValidatorTests()
        {
            config = new ConfigSource();
            config.Hoster = "github";
        }

        [Fact]
        public void ReturnsErrorWhenHosterNotGithub()
        {
            config.Hoster = "foo";

            var sut = new GithubConfigSourceValidator();
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }
    }
}
