using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Configuration
{
    /// <summary>
    /// decorator for ConfigReader, replaces %foo% values with the respective environment variables
    /// </summary>
    internal class EnvironmentVariableConfigReader : IConfigReader
    {
        private readonly IConfigReader configReader;
        private Config config = null;

        public EnvironmentVariableConfigReader(IConfigReader configReader)
        {
            this.configReader = configReader;
        }

        public Config ReadConfig()
        {
            if (this.config != null)
            {
                return this.config;
            }

            var config = this.configReader.ReadConfig();
            
            foreach (var source in config.Sources)
            {
                if (!string.IsNullOrWhiteSpace(source.Password))
                {
                    source.Password = Environment.ExpandEnvironmentVariables(source.Password);    
                }
            }

            this.config = config;
            return config;
        }
    }
}
