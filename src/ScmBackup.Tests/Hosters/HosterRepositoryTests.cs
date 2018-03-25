using ScmBackup.Hosters;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class HosterRepositoryTests
    {
        [Theory]
        [InlineData("foobar", "foobar")]
        [InlineData("foo/bar", "foo#bar")]
        public void InvalidCharsInRepoNameAreReplaced(string inputName, string savedName)
        {
            var sut = new HosterRepository(inputName, "name", "http://clone", ScmType.Git);
            Assert.Equal(savedName, sut.FullName);

            sut = new HosterRepository(inputName, "name", "http://clone", ScmType.Git, false, "", false, "");
            Assert.Equal(savedName, sut.FullName);
        }

        [Fact]
        public void SetWikiWorks()
        {
            var sut = new HosterRepository("foo", "foo", "http://clone", ScmType.Git);
            sut.SetWiki(true, "url");

            Assert.True(sut.HasWiki);
            Assert.Equal("url", sut.WikiUrl);
        }

        [Fact]
        public void SetIssuesWorks()
        {
            var sut = new HosterRepository("foo", "foo", "http://clone", ScmType.Git);
            sut.SetIssues(true, "url");

            Assert.True(sut.HasIssues);
            Assert.Equal("url", sut.IssueUrl);
        }
    }
}
