using ScmBackup.Http;
using System.Collections.Generic;

namespace ScmBackup.Hosters
{
    /// <summary>
    /// marker interface for hoster's API, gets the list of repositories to clone
    /// </summary>
    internal interface IHosterApi
    {
        /// <summary>
        /// result of last API call for testing
        /// </summary>
        HttpResult LastResult { get; }

        List<HosterRepository> GetRepositoryList(ConfigSource config);
    }
}
