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
    }
}
