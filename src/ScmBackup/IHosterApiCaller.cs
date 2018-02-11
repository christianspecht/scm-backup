using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal interface IHosterApiCaller
    {
        List<HosterRepository> GetRepositoryList(ConfigSource source);
    }
}
