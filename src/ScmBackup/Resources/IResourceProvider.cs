using System.Globalization;

namespace ScmBackup.Resources
{
    /// <summary>
    /// Provides translated string resources
    /// </summary>
    public interface IResourceProvider
    {
        void Initialize(CultureInfo culture);

        string GetString(string key);
    }
}
