using System;

namespace ScmBackup
{
    /// <summary>
    /// decorator for ConfigReader, validates the configuration values
    /// </summary>
    internal class ValidatingConfigReader : IConfigReader
    {
        private readonly IConfigReader configReader;
        private readonly ILogger logger;

        public ValidatingConfigReader(IConfigReader configReader, ILogger logger)
        {
            this.configReader = configReader;
            this.logger = logger;
        }

        public Config ReadConfig()
        {
            var config = this.configReader.ReadConfig();

            if (String.IsNullOrWhiteSpace(config.LocalFolder))
            {
                this.logger.Log(LogLevel.Error, "Local Folder is missing!");
                return null;
            }

            return config;
        }
    }
}
