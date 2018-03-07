using System.Collections.Generic;
using System.IO;

namespace ScmBackup
{
    internal class ConfigBackupMaker : IConfigBackupMaker
    {
        private readonly IContext context;

        public ConfigBackupMaker(IContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// subfolder where the configs are saved
        /// </summary>
        public string SubFolder
        {
            get { return "_config"; }
        }

        /// <summary>
        /// File names of the config files to backup
        /// </summary>
        public List<string> ConfigFileNames
        {
            get { return new List<string> { "settings.yml", "NLog.config" }; }
        }

        /// <summary>
        /// Copies important config files into the backup folder.
        /// </summary>
        public void BackupConfigs()
        {
            string backupDir = Path.Combine(this.context.Config.LocalFolder, this.SubFolder);
            Directory.CreateDirectory(backupDir);

            foreach (var file in this.ConfigFileNames)
            {
                File.Copy(file, Path.Combine(backupDir, file), true);
            }
        }
    }
}
