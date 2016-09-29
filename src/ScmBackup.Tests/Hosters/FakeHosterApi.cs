using ScmBackup.Hosters;
using ScmBackup.Http;
using System.Collections.Generic;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterApi : IHosterApi
    {
        public bool WasCalled { get; private set;}

        public List<HosterRepository> RepoList { get; set; }

        public HttpResult LastResult { get; set; }

        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            this.WasCalled = true;
            return this.RepoList;
        }
    }
}
