using System;
using System.Reflection;

namespace ScmBackup
{
    /// <summary>
    /// "application context" for global information
    /// </summary>
    internal class Context : IContext
    {
        public Context()
        {
            var assembly = typeof(ScmBackup).GetTypeInfo().Assembly;
            this.VersionNumber = assembly.GetName().Version;
            this.VersionNumberString= assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            this.AppTitle = Resource.GetString("AppTitle") + " " + this.VersionNumberString;
        }

        public Version VersionNumber { get; private set; }

        public string VersionNumberString { get; private set; }

        public string AppTitle { get; private set; }
    }
}
