using ScmBackup.Resources;
using System;
using System.Globalization;

namespace ScmBackup
{
    /// <summary>
    /// Provides static access to localized string resources
    /// </summary>
    internal static class Resource
    {
        private static IResourceProvider internalProvider = null;
        
        public static void ResetProvider()
        {
            internalProvider = null;
        }

        public static void Initialize(IResourceProvider provider, CultureInfo culture)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider is null");
            }

            internalProvider = provider;
            provider.Initialize(culture);
        }

        public static string GetString(string key)
        {
            if (internalProvider == null)
            {
                throw new ArgumentNullException("internal provider is null");
            }

            return internalProvider.GetString(key);
        }
    }
}
