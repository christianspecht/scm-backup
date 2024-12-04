using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Http;
using System.Collections.Generic;
using System.IO;

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

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public string Backup(ConfigSource source, IEnumerable<HosterRepository> repos, string projectFolder )
        {
            this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Source, source.Title);

            string sourceFolder = this.fileHelper.CreateSubDirectory(context.Config.LocalFolder, source.Title + Path.DirectorySeparatorChar + projectFolder);

            var url = new UrlHelper();

            foreach (var repo in repos)
            {
                string repoFolder = this.fileHelper.CreateSubDirectory(sourceFolder, repo.FullName);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, repo.Scm.ToString(), url.RemoveCredentialsFromUrl(repo.CloneUrl));

                this.backupMaker.MakeBackup(source, repo, repoFolder);

                if (this.context.Config.Options.Backup.LogRepoFinished)
                {
                    this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_RepoFinished);
                }
            }

            return sourceFolder;
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public string Backup( ConfigSource source, IEnumerable<HosterProject> projects )
        {

            this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Source, source.Title);

            string sourceFolder = this.fileHelper.CreateSubDirectory(context.Config.LocalFolder, source.Title);

            var url = new UrlHelper();

            foreach (var project in projects)
            {
                string projectFolder = this.fileHelper.CreateSubDirectory(sourceFolder, project.Key + "#" + project.FullName);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, project.Key.ToString(), url.RemoveCredentialsFromUrl(project.FullName));

                this.backupMaker.MakeBackup(source, project, projectFolder, this.logger);

                if (this.context.Config.Options.Backup.LogRepoFinished)
                {
                    this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_RepoFinished);
                }
            }

            return sourceFolder;

        }
    }
}
