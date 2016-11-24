using System;
using Xunit;

namespace ScmBackup.Tests
{
    public class TestHelperTests
    {
        [Fact]
        public void EnvVar_ReturnsExistingVariable()
        {
            Environment.SetEnvironmentVariable("ThisVariableExists", "foo");

            Assert.Equal("foo", TestHelper.EnvVar("ThisVariableExists"));
        }

        [Fact]
        public void EnvVar_ThrowsWhenRequestedVariableDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() => TestHelper.EnvVar("ThisVariableDoesNotExist"));
        }
    }
}
