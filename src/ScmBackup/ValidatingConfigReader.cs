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
        private readonly IHosterFactory factory;

        public ValidatingConfigReader(IConfigReader configReader, ILogger logger, IHosterFactory factory)
        {
            this.configReader = configReader;
            this.logger = logger;
            this.factory = factory;
        }

        public Config ReadConfig()
        {
            this.logger.Log(ErrorLevel.Debug, Resource.GetString("ReadingConfig"), "ValidatingConfigReader");
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
                var hoster = this.factory.Create(source.Hoster);
                var result = hoster.Validator.Validate(source);

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

            this.logger.Log(ErrorLevel.Debug, Resource.GetString("Return"), "ValidatingConfigReader");
            return config;
        }
    }
}
