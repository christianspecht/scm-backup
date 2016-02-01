using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Holds all configuration values
    /// </summary>
    internal class Config
    {
        public string LocalFolder { get; set; }
        public List<Source> Sources { get; set; }

        public Config()
        {
            this.Sources = new List<Source>();
        }

        internal class Source
        {
            public string Hoster { get; set; }
            public string Type { get; set; }
            public string Name { get; set; }
        }
    }
}
