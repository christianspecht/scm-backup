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

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null )
        {
            var list = this.caller.GetRepositoryList(source, keyProject);

            if (source.IncludeRepos != null && source.IncludeRepos.Any())
            {
                list.RemoveAll(l => !source.IncludeRepos.Contains(l.ShortName));
            }

            return list;
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
         public List<HosterProject> GetProjectsList(ConfigSource source)
        {

            return this.caller.GetProjectsList( source );

        }
    }
}
