using ScmBackup.Scm;
using System;

namespace ScmBackup
{
    /// <summary>
    /// main program execution
    /// </summary>
    internal class ScmBackup : IScmBackup
    {
        private readonly IContext context;
        private readonly IApiCaller apiCaller;
        private readonly IScmValidator validator;
        private readonly IBackupMaker backupMaker;

        public ScmBackup(IContext context, IApiCaller apiCaller, IScmValidator validator, IBackupMaker backupMaker)
        {
            this.context = context;
            this.apiCaller = apiCaller;
            this.validator = validator;
            this.backupMaker = backupMaker;
        }

        public void Run()
        {
            var config = this.context.Config;

            var repos = this.apiCaller.CallApis(config);

            if (!this.validator.ValidateScms(repos.GetScmTypes()))
            {
                throw new InvalidOperationException(Resource.ScmValidatorError);
            }
            
            foreach (var source in repos.GetSources())
            {
                this.backupMaker.Backup(config, source, repos.GetReposForSource(source));
            }
        }
    }
}
