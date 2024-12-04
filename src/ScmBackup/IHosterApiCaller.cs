using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal interface IHosterApiCaller
    {
        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null);
        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        List<HosterProject> GetProjectsList( ConfigSource source );
    }
}
