using ScmBackup.Configuration;
using ScmBackup.Scm;
using System;

namespace ScmBackup
{
    /// <summary>
    /// main program execution
    /// </summary>
    internal class ScmBackup : IScmBackup
    {
        private readonly IApiCaller apiCaller;
        private readonly IScmValidator validator;
        private readonly IBackupMaker backupMaker;
        private readonly IConfigBackupMaker configBackupMaker;
        private readonly IDeletedRepoHandler deletedHandler;

        public ScmBackup(IApiCaller apiCaller, IScmValidator validator, IBackupMaker backupMaker, IConfigBackupMaker configBackupMaker, IDeletedRepoHandler deletedHandler)
        {
            this.apiCaller = apiCaller;
            this.validator = validator;
            this.backupMaker = backupMaker;
            this.configBackupMaker = configBackupMaker;
            this.deletedHandler = deletedHandler;
        }

        public bool Run()
        {
            this.configBackupMaker.BackupConfigs();

            var repos = this.apiCaller.CallApis();

            if (!this.validator.ValidateScms(repos.GetScmTypes()))
            {
                throw new InvalidOperationException(Resource.ScmValidatorError);
            }
            
            foreach (var source in repos.GetSources())
            {
                var sourceRepos = repos.GetReposForSource(source);
                string sourceFolder = this.backupMaker.Backup(source, sourceRepos);
                this.deletedHandler.HandleDeletedRepos(sourceRepos, sourceFolder);
            }

            return true;
        }
    }
}
