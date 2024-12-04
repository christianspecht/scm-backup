using System.Runtime.InteropServices;

namespace ScmBackup
{
    internal class LoggingScmBackup : IScmBackup
    {
        private readonly IScmBackup backup;
        private readonly IContext context;
        private readonly ILogger logger;

        public LoggingScmBackup(IScmBackup backup, IContext context, ILogger logger)
        {
            this.backup = backup;
            this.context = context;
            this.logger = logger;
        }

        public bool Run()
        {
            logger.Log(ErrorLevel.Info, this.context.AppTitle); //SCM Backup 0.0.0-DEV
            logger.Log(ErrorLevel.Info, Resource.AppWebsite); //https://scm-backup.org/
            logger.Log(ErrorLevel.Info, string.Format(Resource.SystemOS, RuntimeInformation.OSDescription)); //Operating system: Microsoft Windows 10.0.22631
            // TODO: log more stuff (configuration...)

            var result = this.backup.Run();

            logger.Log(ErrorLevel.Info, Resource.BackupFinished);
            logger.Log(ErrorLevel.Info, string.Format(Resource.BackupFinishedDirectory, this.context.Config.LocalFolder));

            return result;
        }
    }
}
