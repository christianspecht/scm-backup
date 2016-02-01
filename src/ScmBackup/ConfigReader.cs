using Microsoft.Extensions.Configuration;

namespace ScmBackup
{
    /// <summary>
    /// Reads the configuration values and returns an instance of the Config class
    /// </summary>
    internal class ConfigReader : IConfigReader
    {
        public Config ReadConfig()
        {
            var config = new Config();

            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("settings.json");
            var settings = builder.Build();

            ConfigurationBinder.Bind(settings, config);

            return config;
        }
    }
}
