using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ScmBackup.Configuration
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
            this.ConfigFileName = "settings.yml";
        }

        public Config ReadConfig()
        {
            if (this.config == null)
            {
                this.config = new Config();

                var input = File.ReadAllText(this.ConfigFileName);
                var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                this.config = deserializer.Deserialize<Config>(input);
            }

            return this.config;
        }
    }
}
