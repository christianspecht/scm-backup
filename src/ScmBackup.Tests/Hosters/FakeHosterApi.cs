using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterApi : IHosterApi
    {
        public bool WasCalled { get; private set;}

        public List<HosterRepository> RepoList { get; set; }

        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            this.WasCalled = true;
            return this.RepoList;
        }
    }
}
