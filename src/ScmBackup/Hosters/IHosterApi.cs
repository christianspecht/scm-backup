using ScmBackup.Http;
using System.Collections.Generic;

namespace ScmBackup.Hosters
{
    /// <summary>
    /// marker interface for hoster's API, gets the list of repositories to clone
    /// </summary>
    internal interface IHosterApi
    {
        List<HosterRepository> GetRepositoryList(ConfigSource config);
    }
}
