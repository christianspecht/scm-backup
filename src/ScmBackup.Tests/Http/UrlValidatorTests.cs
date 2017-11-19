using ScmBackup.Http;
using Xunit;

namespace ScmBackup.Tests.Http
{
    public class UrlValidatorTests
    {
        [Theory]
        [InlineData("http://scm-backup.org", true)]
        [InlineData("https://github.com", true)]
        [InlineData("foo", false)]
        [InlineData("file:///c:/foo.txt", false)]
        public void ValidatesUrls(string url, bool expectedResult)
        {
            var sut = new UrlValidator();
            Assert.Equal(expectedResult, sut.UrlIsValid(url));
        }
        
    }
}
