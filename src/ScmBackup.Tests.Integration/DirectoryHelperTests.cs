using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class DirectoryHelperTests
    {
        [Fact]
        public void DirectoryIsCreated()
        {
            var result = DirectoryHelper.CreateTempDirectory();

            Assert.False(string.IsNullOrWhiteSpace(result));
            Assert.True(Directory.Exists(result));
        }

        [Fact]
        public void DirectoryWithSuffixIsCreated()
        {
            string suffix = "foo";

            var result = DirectoryHelper.CreateTempDirectory(suffix);

            Assert.False(string.IsNullOrWhiteSpace(result));
            Assert.True(Directory.Exists(result));
            Assert.True(result.EndsWith(suffix));
        }
    }
}
