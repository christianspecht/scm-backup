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
            { "ApiGettingUrl", "{0}: Getting {1}" },
            { "AppTitle", "SCM Backup" },
            { "BackupFailed", "Backup failed!" },
            { "ConfigSourceIsNull", "ConfigSource must not be NULL" },
            { "ConfigSourceWithoutTitle", "All sources in the settings file must have a title." },
            { "EndSeconds", "The application will close in {0} seconds!" },
            { "HttpRequest", "Http Request: {0}" },
            { "HttpHeaders", "Http Headers: {0}" },
            { "HttpResult", "Http Result: {0}" },
            { "ReadingConfig", "{0}: Reading config" },
            { "StartingBackup", "{0}: Starting backup" },
            { "ApiAuthenticationFailed", "Authentication failed for {0}" },
            { "ApiInvalidUsername", "User {0} not found!" },
            { "ApiMissingPermissions", "Required permissions are missing. Make sure you have set the right scopes!" },
            { "AuthNameAndPasswortEmpty", "AuthName and Password are empty" },
            { "AuthNameOrPasswortEmpty", "Only one of AuthName/Password is filled. Make sure you fill both or none!" },
            { "NameEmpty", "name is empty" },
            { "WrongHoster", "wrong hoster: {0}" },
            { "WrongType", "wrong type: {0}" },
            { "HosterDoesntExist", "Hoster {0} doesn't exist" },
            { "LocalFolderMissing", "Local folder is missing!" },
            { "NoSourceConfigured", "No source configured!" },
            { "Return", "{0}: Return" },
            { "TypeIsNoIHoster", "Can't register {0} in the HosterFactory because it doesn't implement IHoster" },
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
