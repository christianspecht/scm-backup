using Microsoft.Extensions.Configuration;

namespace ScmBackup
{
    /// <summary>
    /// Reads the configuration values and returns an instance of the Config class
    /// </summary>
    internal class ConfigReader : IConfigReader
    {
        public string ConfigFileName { get; set; }

        public ConfigReader()
        {
            this.ConfigFileName = "settings.json";
        }

        public Config ReadConfig()
        {
            var config = new Config();

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(this.ConfigFileName);
            var settings = builder.Build();

            ConfigurationBinder.Bind(settings, config);

            return config;
        }
    }
}
