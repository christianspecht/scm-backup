using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal interface IBackupMaker
    {
        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        string Backup(ConfigSource source, IEnumerable<HosterRepository> repos, string projectFolder = "");

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        string Backup( ConfigSource source, IEnumerable<HosterProject> projects );
    }
}
