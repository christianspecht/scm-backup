using ScmBackup.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ScmBackup.Tests.ConfigTests
{
    public class EnvironmentVariableConfigReaderTests
    {
        private IConfigReader sut;
        private FakeConfigReader reader;

        public EnvironmentVariableConfigReaderTests()
        {
            reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            sut = new EnvironmentVariableConfigReader(reader);

            Environment.SetEnvironmentVariable("scmbackup_test", "foo");
        }

        [Theory]
        [InlineData("%scmbackup_test%bar", "foobar")]   // part of the password
        [InlineData("%scmbackup_test%", "foo")]         // whole password
        public void ReplacesInPassword(string originalPw, string changedPw)
        {
            reader.FakeConfig.Sources.First().Password = originalPw;

            var result = sut.ReadConfig();

            Assert.Equal(changedPw, result.Sources.First().Password);
        }

        [Fact]
        public void DoesNothingWhenPasswordContainsNoVariable()
        {
            reader.FakeConfig.Sources.First().Password = "bar";

            var result = sut.ReadConfig();

            Assert.Equal("bar", result.Sources.First().Password);
        }
    }
}
