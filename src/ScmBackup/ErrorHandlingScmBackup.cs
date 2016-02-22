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
        private readonly IConfigReader conf;

        public ErrorHandlingScmBackup(IScmBackup backup, ILogger logger, IConfigReader conf)
        {
            this.backup = backup;
            this.logger = logger;
            this.conf = conf;
        }

        public void Run()
        {
            Config config = null;

            try
            {
                config = this.conf.ReadConfig();
                this.backup.Run();
            }
            catch(Exception ex)
            {
                this.logger.Log(LogLevel.Error, ex, "Backup failed!");

                // Wait as many seconds as defined in the config.
                // If we don't have the config value because the exception was thrown while reading the config, use a fixed value
                int seconds = 5;

                if (config != null)
                {
                    seconds = config.WaitSecondsOnError;
                }

                Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
            }
        }
    }
}
