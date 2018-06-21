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

        [Fact]
        public void EnvVar_DoesNotThrowsWhenRequestedVariableDoesNotExist()
        {
            Assert.Null(TestHelper.EnvVar("ThisVariableDoesNotExist", false));
        }

        [Fact]
        public void EnvVar_WithPrefix_ReturnsExistingVariable()
        {
            Environment.SetEnvironmentVariable("prefix_name", "foo");

            Assert.Equal("foo", TestHelper.EnvVar("prefix", "name"));
        }

        [Fact]
        public void BuildRepositoryName_BuildsName()
        {
            Assert.Equal("user#repo", TestHelper.BuildRepositoryName("user", "repo"));
        }

        [Theory]
        [InlineData("user", null)]
        [InlineData("user", "")]
        [InlineData("user", " ")]
        [InlineData(null, "repo")]
        [InlineData("", "repo")]
        [InlineData(" ", "repo")]
        [InlineData("", "")]
        public void BuildRepositoryName_ThrowsWhenParameterIsEmpty(string userName, string repoName)
        {
            Assert.Throws<ArgumentException>(() => TestHelper.BuildRepositoryName(userName, repoName));
        }
    }
}
