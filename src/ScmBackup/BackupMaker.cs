using ScmBackup.Hosters;
using ScmBackup.Scm;
using System;
using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Backs up all repositories from a single source
    /// </summary>
    internal class BackupMaker : IBackupMaker
    {
        private readonly ILogger logger;
        private readonly IScmFactory scmFactory;
        private readonly IFileSystemHelper fileHelper;

        public BackupMaker(ILogger logger, IScmFactory scmfactory, IFileSystemHelper fileHelper)
        {
            this.logger = logger;
            this.scmFactory = scmfactory;
            this.fileHelper = fileHelper;
        }

        public void Backup(Config config, ConfigSource source, IEnumerable<HosterRepository> repos)
        {
            this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Source, source.Title);

            string sourceFolder = this.fileHelper.CreateSubDirectory(config.LocalFolder, source.Title);

            foreach (var repo in repos)
            {
                string tmp = repo.Scm.ToString();

                var scm = this.scmFactory.Create(repo.Scm);
                if (!scm.IsOnThisComputer(config))
                {
                    throw new InvalidOperationException(string.Format(Resource.ScmNotOnThisComputer, repo.Scm.ToString()));
                }

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, scm.ShortName, repo.CloneUrl);

                string repoFolder = this.fileHelper.PathCombine(sourceFolder, repo.Name);

                scm.PullFromRemote(repo.CloneUrl, repoFolder);

                if (!scm.DirectoryIsRepository(repoFolder))
                {
                    throw new InvalidOperationException(Resource.DirectoryNoRepo);
                }
            }
        }
    }
}
