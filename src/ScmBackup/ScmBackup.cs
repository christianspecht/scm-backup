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

        public ScmBackup(IApiCaller apiCaller, IScmValidator validator, IBackupMaker backupMaker)
        {
            this.apiCaller = apiCaller;
            this.validator = validator;
            this.backupMaker = backupMaker;
        }

        public void Run()
        {
            var repos = this.apiCaller.CallApis();

            if (!this.validator.ValidateScms(repos.GetScmTypes()))
            {
                throw new InvalidOperationException(Resource.ScmValidatorError);
            }
            
            foreach (var source in repos.GetSources())
            {
                this.backupMaker.Backup(source, repos.GetReposForSource(source));
            }
        }
    }
}
