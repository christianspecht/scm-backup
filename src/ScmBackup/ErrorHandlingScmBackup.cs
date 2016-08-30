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

        /// <summary>
        /// default wait time after an error occurs 
        /// (overridable in the tests)
        /// </summary>
        public int WaitSecondsOnError { get; set; }

        public ErrorHandlingScmBackup(IScmBackup backup, ILogger logger, IConfigReader conf)
        {
            this.backup = backup;
            this.logger = logger;
            this.conf = conf;

            this.WaitSecondsOnError = 5;
        }

        public void Run()
        {
            Config config = null;
            bool ok = false;
            string className = this.GetType().Name;

            try
            {
                this.logger.Log(ErrorLevel.Debug, Resource.GetString("ReadingConfig"), className);
                config = this.conf.ReadConfig();

                if (config != null)
                {
                    this.logger.Log(ErrorLevel.Debug, Resource.GetString("StartingBackup"), className);
                    this.backup.Run();
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                this.logger.Log(ErrorLevel.Error, ex.Message);
            }

            if (!ok)
            {
                // Wait as many seconds as defined in the config.
                // If we don't have the config value because the exception was thrown while reading the config, use the default value defined in this class
                int seconds = this.WaitSecondsOnError;

                if (config != null)
                {
                    seconds = config.WaitSecondsOnError;
                }

                this.logger.Log(ErrorLevel.Error, Resource.GetString("BackupFailed"));
                this.logger.Log(ErrorLevel.Error, Resource.GetString("EndSeconds"), seconds);
                Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();
            }
        }
    }
}
