using ScmBackup.Configuration;
using ScmBackup.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScmBackup.Hosters
{
    /// <summary>
    /// marker interface for hoster's API, gets the list of repositories to clone
    /// </summary>
    internal interface IHosterApi
    {
        List<HosterRepository> GetRepositoryList(ConfigSource config,string keyProject = null);

        /*
         * Add by ISC. Gicel Cordoba Pech. 
           Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
           Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        List<HosterProject> GetProjectList( ConfigSource config );
    }
}
