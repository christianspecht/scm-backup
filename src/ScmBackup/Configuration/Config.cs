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
        /// <typeparam name="T">The value must be of this type</typeparam>
        /// <param name="key1">first level key</param>
        /// <param name="key2">second level key</param>
        /// <returns></returns>
        public T GetOption<T>(string key1, string key2)
        {
            if (this.Options == null || !this.Options.ContainsKey(key1))
            {
                return default(T);
            }

            var optionsSub = this.Options[key1];
            if (optionsSub == null || !optionsSub.ContainsKey(key2))
            {
                return default(T);
            }

            var val = optionsSub[key2];
            try
            {
                return (T)Convert.ChangeType(val, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}
