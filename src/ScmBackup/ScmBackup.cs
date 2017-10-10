using ScmBackup.Scm;
using System;

namespace ScmBackup
{
    /// <summary>
    /// main program execution
    /// </summary>
    internal class ScmBackup : IScmBackup
    {
        private readonly IConfigReader reader;
        private readonly IApiCaller apiCaller;
        private readonly IScmValidator validator;
        private readonly IBackupMaker backupMaker;

        public ScmBackup(IConfigReader reader, IApiCaller apiCaller, IScmValidator validator, IBackupMaker backupMaker)
        {
            this.reader = reader;
            this.apiCaller = apiCaller;
            this.validator = validator;
            this.backupMaker = backupMaker;
        }

        public void Run()
        {
            var config = this.reader.ReadConfig();

            var repos = this.apiCaller.CallApis(config);

            if (!this.validator.ValidateScms(repos.GetScmTypes(), config))
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
