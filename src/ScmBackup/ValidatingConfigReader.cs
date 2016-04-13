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
        private readonly IHosterFactory factory;

        public ValidatingConfigReader(IConfigReader configReader, ILogger logger, IHosterFactory factory)
        {
            this.configReader = configReader;
            this.logger = logger;
            this.factory = factory;
        }

        public Config ReadConfig()
        {
            var config = this.configReader.ReadConfig();

            if (String.IsNullOrWhiteSpace(config.LocalFolder))
            {
                this.logger.Log(ErrorLevel.Error, "Local Folder is missing!");
                return null;
            }

            if (config.Sources.Count == 0)
            {
                this.logger.Log(ErrorLevel.Error, "No source configured");
                return null;
            }

            foreach (var source in config.Sources)
            {
                var hoster = this.factory.Create(source.Hoster);
                var result = hoster.Validator.Validate(source);
            }

            return config;
        }
    }
}
