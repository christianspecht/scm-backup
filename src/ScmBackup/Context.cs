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
        private Config config;

        public Context(IConfigReader reader)
        {
            this.reader = reader;

            var assembly = typeof(ScmBackup).GetTypeInfo().Assembly;
            this.VersionNumber = assembly.GetName().Version;
            this.VersionNumberString= assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            this.AppTitle = Resource.AppTitle + " " + this.VersionNumberString;
        }

        public Version VersionNumber { get; private set; }

        public string VersionNumberString { get; private set; }

        public string AppTitle { get; private set; }

        public Config Config
        {
            get
            {
                if (this.config == null)
                {
                    this.Config = this.reader.ReadConfig();
                }

                return this.config;
            }
            private set
            {
                this.config = value;
            }
        }
    }
}