using System;
using Xunit;

namespace ScmBackup.Tests
{
    public class TestHelperTests
    {
        [Fact]
        public void ReturnsExistingVariable()
        {
            Environment.SetEnvironmentVariable("ThisVariableExists", "foo");

            Assert.Equal("foo", TestHelper.EnvVar("ThisVariableExists"));
        }

        [Fact]
        public void ThrowsWhenRequestedVariableDoesNotExist()
        {
            Assert.Throws<ArgumentException>(() => TestHelper.EnvVar("ThisVariableDoesNotExist"));
        }
    }
}
