using ScmBackup.Hosters.Github;

namespace ScmBackup.Tests.Hosters
{
    public class GithubConfigSourceValidatorTests : IConfigSourceValidatorTests
    {
        public GithubConfigSourceValidatorTests()
        {
            config = new ConfigSource();
            config.Hoster = "github";
            config.Type = "user";
            config.Name = "foo";
            config.AuthName = "authname";
            config.Password = "pass";

            sut = new GithubConfigSourceValidator();
        }
    }
}
