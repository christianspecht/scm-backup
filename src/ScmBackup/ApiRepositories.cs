using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<ConfigSource> GetSources()
        {
            return this.Dic.Keys.ToList();
        }

        public List<HosterRepository> GetReposForSource(ConfigSource config)
        {
            return this.Dic[config];
        }

        /// <summary>
        /// Returns a unique list of all ScmTypes from all HosterRepositories
        /// </summary>
        public HashSet<ScmType> GetScmTypes()
        {
            if (!this.Dic.Any())
            {
                throw new InvalidOperationException(Resource.ApiRepositoriesContainsNoHosterRepos);
            }

            var result = new HashSet<ScmType>();

            foreach (var item in this.Dic)
            {
                foreach (var repo in item.Value)
                {
                    result.Add(repo.Scm);
                }
            }

            return result;
        }
    }
}
