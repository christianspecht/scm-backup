using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup
{
    internal interface IDeletedRepoHandler
    {
        IEnumerable<string> HandleDeletedRepos(IEnumerable<HosterRepository> repos, string backupDir);

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        IEnumerable<string> HandleDeleteProjects( IEnumerable<HosterProject> projects, string backupDir );
    }
}
