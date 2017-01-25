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
    }
}
