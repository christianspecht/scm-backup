using System.Collections.Generic;
using System.IO;

namespace ScmBackup.Configuration
{
    internal class ConfigBackupMaker : IConfigBackupMaker
    {
        private readonly IContext context;
        private readonly ILogger logger;

        public ConfigBackupMaker(IContext context, ILogger logger)
        {
            this.context = context;
            this.logger = logger;
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
            get
            {
                var list = this.logger.FilesToBackup;
                list.Add("settings.yml");
                return list;
            }
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

            this.logger.Log(ErrorLevel.Info, Resource.BackingUpConfigs);
        }
    }
}
