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

        public void Run()
        {
            logger.Log(ErrorLevel.Info, this.context.AppTitle);

            // TODO: log more stuff (operating system, configuration...)

            this.backup.Run();
        }
    }
}
