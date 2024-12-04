using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Configuration
{
    /// <summary>
    /// Main class for various options
    /// </summary>
    class ConfigOptions
    {
        public BackupOptions Backup { get; set; } = new BackupOptions();
    }

    class BackupOptions
    {
        public bool RemoveDeletedRepos { get; set; }
        public bool LogRepoFinished { get; set; }

        /*
         * Add by ISC. Gicel Cordoba Pech. 
           Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
           Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public bool BackupByProject { get; set; }
        
        /*
         * Add by ISC. Gicel Cordoba Pech. 
           Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
           Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
       //public bool TokenGitHub { get; set; }
    }
}
