namespace ScmBackup
{
    internal class LoggingScmBackup : IScmBackup
    {
        private readonly IScmBackup backup;
        private readonly IContext context;
        private readonly ILogger logger;
        private readonly IConfigReader reader;

        public LoggingScmBackup(IScmBackup backup, IContext context, ILogger logger, IConfigReader reader)
        {
            this.backup = backup;
            this.context = context;
            this.logger = logger;
            this.reader = reader;
        }

        public void Run()
        {
            logger.Log(ErrorLevel.Info, this.context.AppTitle);
            logger.Log(ErrorLevel.Info, Resource.AppWebsite);

            // TODO: log more stuff (operating system, configuration...)

            this.backup.Run();

            var config = this.reader.ReadConfig();

            logger.Log(ErrorLevel.Info, Resource.BackupFinished);
            logger.Log(ErrorLevel.Info, string.Format(Resource.BackupFinishedDirectory, config.LocalFolder));
        }
    }
}
