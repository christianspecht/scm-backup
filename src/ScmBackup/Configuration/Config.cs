using System;
using System.Collections.Generic;

namespace ScmBackup.Configuration
{
    /// <summary>
    /// Holds all configuration values
    /// </summary>
    internal class Config
    {
        public string LocalFolder { get; set; }

        public int WaitSecondsOnError { get; set; }

        public List<ConfigScm> Scms { get; set; }

        public List<ConfigSource> Sources { get; set; }

        /// <summary>
        /// Various options
        /// </summary>
        public ConfigOptions Options { get; set; }

        public ConfigEmail Email { get; set; }

        public Config()
        {
            this.Sources = new List<ConfigSource>();
            this.Scms = new List<ConfigScm>();
            this.Options = new Dictionary<string, Dictionary<string, object>>();
        }
    }
}
