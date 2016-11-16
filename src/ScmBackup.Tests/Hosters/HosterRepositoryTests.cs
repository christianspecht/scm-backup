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
            var sut = new HosterRepository(inputName, "http://clone", ScmType.Git);

            Assert.Equal(savedName, sut.Name);
        }
    }
}
