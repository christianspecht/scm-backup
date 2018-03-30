using ScmBackup.Hosters;
using System.Collections.Generic;
using System.Linq;

namespace ScmBackup
{
    internal class IgnoringHosterApiCaller : IHosterApiCaller
    {
        private readonly IHosterApiCaller caller;

        public IgnoringHosterApiCaller(IHosterApiCaller caller)
        {
            this.caller = caller;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            var list = this.caller.GetRepositoryList(source);

            if (source.IgnoreRepos != null && source.IgnoreRepos.Any())
            {
                list.RemoveAll(l => source.IgnoreRepos.Contains(l.ShortName));
            }

            return list;
        }
    }
}
