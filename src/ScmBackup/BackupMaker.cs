using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Http;
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
        private readonly IContext context;

        public BackupMaker(ILogger logger, IFileSystemHelper fileHelper, IHosterBackupMaker backupMaker, IContext context)
        {
            this.logger = logger;
            this.fileHelper = fileHelper;
            this.backupMaker = backupMaker;
            this.context = context;
        }

        public string Backup(ConfigSource source, IEnumerable<HosterRepository> repos)
        {
            this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Source, source.Title);

            string sourceFolder = this.fileHelper.CreateSubDirectory(context.Config.LocalFolder, source.Title);

            var url = new UrlHelper();

            foreach (var repo in repos)
            {
                string repoFolder = this.fileHelper.CreateSubDirectory(sourceFolder, repo.FullName);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, repo.Scm.ToString(), url.RemoveCredentialsFromUrl(repo.CloneUrl));

                this.backupMaker.MakeBackup(source, repo, repoFolder);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_RepoFinished);
            }

            return sourceFolder;
        }
    }
}
