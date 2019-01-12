using System.Collections.Generic;

namespace ScmBackup.Configuration
{
    internal interface IConfigBackupMaker
    {
        /// <summary>
        /// subfolder where the configs are saved
        /// </summary>
        string SubFolder { get; }

        /// <summary>
        /// File names of the config files to backup
        /// </summary>
        List<string> ConfigFileNames { get; }

        /// <summary>
        /// Copies important config files into the backup folder.
        /// </summary>
        void BackupConfigs();
    }
}
