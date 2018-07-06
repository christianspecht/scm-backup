using ScmBackup.Http;
using Xunit;

namespace ScmBackup.Tests.Http
{
    public class UrlHelperTests
    {
        [Theory]
        [InlineData("http://scm-backup.org", true)]
        [InlineData("https://github.com", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("foo", false)]
        [InlineData("file:///c:/foo.txt", false)]
        public void ValidatesUrls(string url, bool expectedResult)
        {
            var sut = new UrlHelper();
            Assert.Equal(expectedResult, sut.UrlIsValid(url));
        }

        [Theory]
        [InlineData("http://user:pass@example.com/", "http://example.com/")]
        [InlineData("http://user@example.com/", "http://example.com/")]
        [InlineData("https://user:pass@example.com/", "https://example.com/")]
        [InlineData("https://user@example.com/", "https://example.com/")]
        [InlineData("https://example.com/", "https://example.com/")]
        public void RemovesCredentials(string oldUrl, string expectedUrl)
        {
            var sut = new UrlHelper();
            Assert.Equal(expectedUrl, sut.RemoveCredentialsFromUrl(oldUrl));

        }
        
    }
}
