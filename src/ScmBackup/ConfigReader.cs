using Microsoft.Extensions.Configuration;

namespace ScmBackup
{
    /// <summary>
    /// Reads the configuration values and returns an instance of the Config class
    /// </summary>
    internal class ConfigReader : IConfigReader
    {
        public string ConfigFileName { get; set; }

        private Config config = null;

        public ConfigReader()
        {
            this.ConfigFileName = "settings.json";
        }

        public Config ReadConfig()
        {
            if (this.config == null)
            {
                this.config = new Config();

                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(this.ConfigFileName);
                var settings = builder.Build();

                ConfigurationBinder.Bind(settings, this.config);
            }

            return this.config;
        }
    }
}
