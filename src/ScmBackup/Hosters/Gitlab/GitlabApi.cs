using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabApi : IHosterApi
    {
        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            throw new NotImplementedException();
        }
    }
}
