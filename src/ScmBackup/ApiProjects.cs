/*
    * Add by ISC. Gicel Cordoba Pech. 
    Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
    Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
*/
using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScmBackup
{
    internal class ApiProjects
    {
        public Dictionary<ConfigSource, List<HosterProject>> Dic { get; private set; }

        public ApiProjects()
        {
            this.Dic = new Dictionary<ConfigSource, List<HosterProject>>();
        }

        public void AddItem(ConfigSource config, List<HosterProject> projects )
        {
            this.Dic.Add(config, projects);
        }

        public IEnumerable<ConfigSource> GetSources()
        {
            return this.Dic.Keys.ToList();
        }

        public IEnumerable<HosterProject> GetProjectsForSource(ConfigSource config)
        {
            return this.Dic[config].OrderBy(r => r.FullName);
        }

    }
}
