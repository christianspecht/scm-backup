using ScmBackup.Hosters;
using ScmBackup.Scm;
using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Backs up all repositories from a single source
    /// </summary>
    internal class BackupMaker : IBackupMaker
    {
        private readonly ILogger logger;
        private readonly IFileSystemHelper fileHelper;
        private readonly IHosterBackupMaker backupMaker;

        public BackupMaker(ILogger logger, IFileSystemHelper fileHelper, IHosterBackupMaker backupMaker)
        {
            this.logger = logger;
            this.fileHelper = fileHelper;
            this.backupMaker = backupMaker;
        }

        public void Backup(Config config, ConfigSource source, IEnumerable<HosterRepository> repos)
        {
            this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Source, source.Title);

            string sourceFolder = this.fileHelper.CreateSubDirectory(config.LocalFolder, source.Title);

            foreach (var repo in repos)
            {
                string repoFolder = this.fileHelper.CreateSubDirectory(sourceFolder, repo.Name);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, repo.Scm.ToString(), repo.CloneUrl);

                this.backupMaker.MakeBackup(source, repo, repoFolder);
            }
        }
    }
}
