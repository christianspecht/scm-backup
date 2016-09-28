using System;
using System.Linq;

namespace ScmBackup
{
    /// <summary>
    /// decorator for ConfigReader, validates the configuration values
    /// </summary>
    internal class ValidatingConfigReader : IConfigReader
    {
        private readonly IConfigReader configReader;
        private readonly ILogger logger;
        private readonly IHosterValidator validator;

        private Config config = null;

        public ValidatingConfigReader(IConfigReader configReader, ILogger logger, IHosterValidator validator)
        {
            this.configReader = configReader;
            this.logger = logger;
            this.validator = validator;
        }

        public Config ReadConfig()
        {
            if (this.config != null)
            {
                return this.config;
            }

            string className = this.GetType().Name;
            this.logger.Log(ErrorLevel.Debug, Resource.GetString("ReadingConfig"), className);
            var config = this.configReader.ReadConfig();

            if (String.IsNullOrWhiteSpace(config.LocalFolder))
            {
                this.logger.Log(ErrorLevel.Error, Resource.GetString("LocalFolderMissing"));
                return null;
            }

            if (config.Sources.Count == 0)
            {
                this.logger.Log(ErrorLevel.Error, "NoSourceConfigured");
                return null;
            }

            foreach (var source in config.Sources)
            {
                var result = this.validator.Validate(source);

                if (result.Messages.Any())
                {
                    foreach (var message in result.Messages)
                    {
                        this.logger.Log(message.Error, message.Message);
                    }
                }

                if (!result.IsValid)
                {
                    return null;
                }
            }

            this.logger.Log(ErrorLevel.Debug, Resource.GetString("Return"), className);
            this.config = config;
            return config;
        }
    }
}
