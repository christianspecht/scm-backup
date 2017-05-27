using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class TempDirectoryHelperTests
    {
        [Fact]
        public void DirectoryIsCreated()
        {
            var result = TempDirectoryHelper.CreateTempDirectory();

            Assert.False(string.IsNullOrWhiteSpace(result));
            Assert.True(Directory.Exists(result));
        }

        [Fact]
        public void DirectoryWithSuffixIsCreated()
        {
            string suffix = "foo";

            var result = TempDirectoryHelper.CreateTempDirectory(suffix);

            Assert.False(string.IsNullOrWhiteSpace(result));
            Assert.True(Directory.Exists(result));
            Assert.True(result.EndsWith(suffix));
        }
    }
}
