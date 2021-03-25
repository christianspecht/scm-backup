using ScmBackup.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScmBackup.Tests.Configuration
{
    public class ConfigTests
    {
        private Config sut;

        public ConfigTests()
        {
            this.sut = new Config();
        }

        [Fact]
        public void IsInitialized()
        {
            Assert.NotNull(sut.Options);
            Assert.NotNull(sut.Options.Git);
            Assert.NotNull(sut.Scms);
            Assert.NotNull(sut.Sources);
        }
    }
}
