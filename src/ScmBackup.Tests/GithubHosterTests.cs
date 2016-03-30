using ScmBackup.Hosters;
using Xunit;

namespace ScmBackup.Tests
{
    public class GithubHosterTests
    {
        [Fact]
        public void ValidatorIsSet()
        {
            var sut = new GithubHoster();

            Assert.NotNull(sut.Validator);
        }
    }
}
