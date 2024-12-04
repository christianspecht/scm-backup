using ScmBackup.Configuration;
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

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null)
        {
            var list = this.caller.GetRepositoryList(source, keyProject);

            if (source.IgnoreRepos != null && source.IgnoreRepos.Any())
            {
                list.RemoveAll(l => source.IgnoreRepos.Contains(l.ShortName));
            }

            return list;
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterProject> GetProjectsList( ConfigSource source )
        {

            return this.caller.GetProjectsList(source);
            
        }
    }

}
