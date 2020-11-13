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
        /// Two-level "generic" dictionary for various options
        /// </summary>
        public Dictionary<string, Dictionary<string, object>> Options { get; set; }

        public ConfigEmail Email { get; set; }

        public Config()
        {
            this.Sources = new List<ConfigSource>();
            this.Scms = new List<ConfigScm>();
        }

        /// <summary>
        /// Gets a value from the config options
        /// </summary>
        /// <param name="type">The value must be of this type</param>
        /// <param name="key1">first level key</param>
        /// <param name="key2">second level key</param>
        /// <returns></returns>
        public object GetOption(Type type, string key1, string key2)
        {
            if (this.Options == null || !this.Options.ContainsKey(key1))
            {
                return null;
            }

            var optionsSub = this.Options[key1];
            if (optionsSub == null || !optionsSub.ContainsKey(key2))
            {
                return null;
            }

            var val = optionsSub[key2];
            try
            {
                return Convert.ChangeType(val, type);
            }
            catch
            {
                return null;
            }
        }
    }
}
