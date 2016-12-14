using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal class ApiRepositories
    {
        public Dictionary<ConfigSource, List<HosterRepository>> Dic { get; private set; }

        public ApiRepositories()
        {
            this.Dic = new Dictionary<ConfigSource, List<HosterRepository>>();
        }

        public void AddItem(ConfigSource config, List<HosterRepository> repos)
        {
            this.Dic.Add(config, repos);
        }

        public List<HosterRepository> GetReposForSource(ConfigSource config)
        {
            return this.Dic[config];
        }
    }
}
