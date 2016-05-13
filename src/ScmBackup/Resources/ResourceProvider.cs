using System;
using System.Collections.Generic;
using System.Globalization;

namespace ScmBackup.Resources
{
    /// <summary>
    /// Provides translated string resources
    /// </summary>
    public class ResourceProvider : IResourceProvider
    {
        private bool initialized;
        private CultureInfo culture;

        /// <summary>
        /// temporary list of resources until we have a better solution
        /// </summary>
        private Dictionary<string, string> resources = new Dictionary<string, string>
        {
            { "AppTitle", "SCM Backup" }
        };

        public void Initialize(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new InvalidOperationException("Invalid culture!");
            }

            this.culture = culture;
            this.initialized = true;
        }

        public string GetString(string key)
        {
            if (!this.initialized)
            {
                throw new InvalidOperationException("ResourceProvider not initialized!");
            }

            string result;
            this.resources.TryGetValue(key, out result);
            return result;
        }
    }
}
