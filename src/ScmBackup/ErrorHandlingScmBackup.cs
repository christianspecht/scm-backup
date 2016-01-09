using System;
using System.Threading.Tasks;

namespace ScmBackup
{
    /// <summary>
    /// decorator for ScmBackup, handles and logs errors
    /// </summary>
    internal class ErrorHandlingScmBackup : IScmBackup
    {
        private readonly IScmBackup backup;
        private readonly ILogger logger;

        public ErrorHandlingScmBackup(IScmBackup backup, ILogger logger)
        {
            this.backup = backup;
            this.logger = logger;
        }

        public void Run()
        {
            try
            {
                this.backup.Run();
            }
            catch(Exception ex)
            {
                this.logger.Log(LogLevel.Error, ex, "Backup failed!");
                Task.Delay(TimeSpan.FromSeconds(3)).Wait();
            }
        }
    }
}
