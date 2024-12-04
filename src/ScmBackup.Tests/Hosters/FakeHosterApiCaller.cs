using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System;
using System.Collections.Generic;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterApiCaller : IHosterApiCaller
    {
        public Dictionary<ConfigSource, List<HosterRepository>> Lists { get; private set; }
        public List<ConfigSource> PassedConfigSources { get; private set; }

        public FakeHosterApiCaller()
        {
            this.Lists = new Dictionary<ConfigSource, List<HosterRepository>>();
            this.PassedConfigSources = new List<ConfigSource>();
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterProject> GetProjectList( ConfigSource source )
        {
            var listProject = new List<HosterProject>();

            return listProject;
        }

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null)
        {
            if (this.Lists == null || this.Lists.Count == 0)
            {
                throw new InvalidOperationException("dictionary is empty");
            }

            this.PassedConfigSources.Add(source);
            return this.Lists[source];
        }
    }
}
