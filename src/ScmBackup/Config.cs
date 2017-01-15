using System.Collections.Generic;

namespace ScmBackup
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

        public Config()
        {
            this.Sources = new List<ConfigSource>();
            this.Scms = new List<ConfigScm>();
        }
    }
}
