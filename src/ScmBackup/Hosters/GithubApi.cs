using System;
using System.Collections.Generic;

namespace ScmBackup.Hosters
{
    /// <summary>
    /// Calls the GitHub API
    /// </summary>
    internal class GithubApi : IGithubApi
    {
        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            throw new NotImplementedException();
        }
    }
}
