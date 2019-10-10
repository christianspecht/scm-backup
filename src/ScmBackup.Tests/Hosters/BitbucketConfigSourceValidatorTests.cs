using ScmBackup.Hosters.Bitbucket;

namespace ScmBackup.Tests.Hosters
{
    public class BitbucketConfigSourceValidatorTests : IConfigSourceValidatorTests
    {
        public BitbucketConfigSourceValidatorTests()
        {
            config = new ConfigSource();
            config.Hoster = "bitbucket";
            config.Type = "user";
            config.Name = "foo";
            config.AuthName = config.Name;
            config.Password = "pass";

            sut = new BitbucketConfigSourceValidator();
        }
    }
}
