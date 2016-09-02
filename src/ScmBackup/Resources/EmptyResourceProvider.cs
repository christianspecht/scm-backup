using System.Globalization;

namespace ScmBackup.Resources
{
    // default resource provider when nothing else is set
    public class EmptyResourceProvider : IResourceProvider
    {
        public string GetString(string key)
        {
            return key;
        }

        public void Initialize(CultureInfo culture) { }
    }
}
