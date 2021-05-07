using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System;
using System.Collections.Generic;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterApiCaller : IHosterApiCaller
    {
        public Dictionary<ConfigSource, List<HosterRepository>> Lists { get; private set; }
        public List<ConfigSource> PassedConfigSources { get; private set; }

        public FakeHosterApiCaller()
        {
            this.Lists = new Dictionary<ConfigSource, List<HosterRepository>>();
            this.PassedConfigSources = new List<ConfigSource>();
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            if (this.Lists == null || this.Lists.Count == 0)
            {
                throw new InvalidOperationException("dictionary is empty");
            }

            this.PassedConfigSources.Add(source);
            return this.Lists[source];
        }
    }
}
