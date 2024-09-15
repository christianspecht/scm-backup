using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScmBackup.Configuration
{
    /// <summary>
    /// decorator for ConfigReader, replaces %foo% values with the respective environment variables
    /// </summary>
    internal class AddTimestampedSubfolderConfigReader : IConfigReader
    {
        private readonly IConfigReader configReader;
		private readonly IFileSystemHelper fileHelper;
		private Config config = null;

        public AddTimestampedSubfolderConfigReader(IConfigReader configReader, IFileSystemHelper fileHelper)
        {
            this.configReader = configReader;
            this.fileHelper = fileHelper;
        }

        public Config ReadConfig()
        {
            if (this.config != null)
            {
                return this.config;
            }

            var config = this.configReader.ReadConfig();

			if (config.Options.Backup.AddTimestampedSubfolder) {
                string localFolder = config.LocalFolder;
                string timestampFormat = config.Options.Backup.TimestampFormat;
				string timestamp = DateTime.Now.ToString(timestampFormat);
                this.fileHelper.CreateSubDirectory(localFolder, timestamp);
                config.LocalFolder = Path.Combine(localFolder, timestamp);
			}
			
            this.config = config;
            return config;
        }
    }
}
