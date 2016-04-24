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
            bool ok = false;

            try
            {
                this.logger.Log(ErrorLevel.Debug, "ErrorHandlingScmBackup: Reading config");
                config = this.conf.ReadConfig();

                if (config != null)
                {
                    this.logger.Log(ErrorLevel.Debug, "ErrorHandlingScmBackup: Starting backup");
                    this.backup.Run();
                    ok = true;
                }
            }
            catch(Exception ex)
            {
                this.logger.Log(ErrorLevel.Error, ex.Message);
            }

            if (!ok)
            {
                // Wait as many seconds as defined in the config.
                // If we don't have the config value because the exception was thrown while reading the config, use a fixed value
                int seconds = 5;

                if (config != null)
                {
                    seconds = config.WaitSecondsOnError;
                }

                this.logger.Log(ErrorLevel.Error, "Backup failed!");
                this.logger.Log(ErrorLevel.Error, "ScmBackup will end in {0} seconds!", seconds);
                Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
            }
        }
    }
}
