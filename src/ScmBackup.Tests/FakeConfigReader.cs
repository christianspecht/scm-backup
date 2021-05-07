using ScmBackup.Configuration;
using System;

namespace ScmBackup.Tests
{
    internal class FakeConfigReader : IConfigReader
    {
        public Config FakeConfig { get; set; }

        public Config ReadConfig()
        {
            if (this.FakeConfig == null)
            {
                throw new InvalidOperationException();
            }

            return this.FakeConfig;
        }

        public void SetDefaultFakeConfig()
        {
            var config = new Config();
            config.LocalFolder = "foo";
            config.WaitSecondsOnError = 0;

            var source = new ConfigSource();
            source.Title = "title";
            source.Hoster = "fake";

            config.Sources.Add(source);

            this.FakeConfig = config;
        }
    }
}
