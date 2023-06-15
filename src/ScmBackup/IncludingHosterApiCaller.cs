using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;
using System.Linq;

namespace ScmBackup
{
    internal class IncludingHosterApiCaller : IHosterApiCaller
    {
        private readonly IHosterApiCaller caller;

        public IncludingHosterApiCaller(IHosterApiCaller caller)
        {
            this.caller = caller;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = this.caller.GetRepositoryList(source);

            if (source.IncludeRepos != null && source.IncludeRepos.Any())
            {
                list.RemoveAll(l => !source.IncludeRepos.Contains(l.ShortName));
            }

            return list;
        }
    }
}
