using System;
using System.Reflection;

namespace ScmBackup
{
    /// <summary>
    /// "application context" for global information
    /// </summary>
    internal class Context : IContext
    {
        private readonly IConfigReader reader;

        public Context(IConfigReader reader)
        {
            this.reader = reader;
            this.Config = this.reader.ReadConfig();

            var assembly = typeof(ScmBackup).GetTypeInfo().Assembly;
            this.VersionNumber = assembly.GetName().Version;
            this.VersionNumberString= assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            this.AppTitle = Resource.AppTitle + " " + this.VersionNumberString;
        }

        public Version VersionNumber { get; private set; }

        public string VersionNumberString { get; private set; }

        public string AppTitle { get; private set; }

        public Config Config { get; private set; }
    }
}
