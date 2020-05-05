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
        private readonly IContext context;

        /// <summary>
        /// default wait time after an error occurs 
        /// (overridable in the tests)
        /// </summary>
        public int WaitSecondsOnError { get; set; }

        public ErrorHandlingScmBackup(IScmBackup backup, ILogger logger, IContext context)
        {
            this.backup = backup;
            this.logger = logger;
            this.context = context;

            this.WaitSecondsOnError = 5;
        }

        public bool Run()
        {
            string className = this.GetType().Name;

            try
            {
                this.logger.Log(ErrorLevel.Debug, Resource.StartingBackup, className);
                return this.backup.Run();
            }
            catch (Exception ex)
            {
                this.logger.Log(ErrorLevel.Error, ex.Message);

                // Wait as many seconds as defined in the config.
                // If we don't have the config value because the exception was thrown while reading the config, use the default value defined in this class
                int seconds = this.WaitSecondsOnError;

                if (this.context.Config != null)
                {
                    seconds = this.context.Config.WaitSecondsOnError;
                }

                this.logger.Log(ErrorLevel.Error, Resource.BackupFailed);
                this.logger.Log(ErrorLevel.Error, Resource.EndSeconds, seconds);
                Task.Delay(TimeSpan.FromSeconds(seconds)).Wait();

                return false;
            }
        }
    }
}
