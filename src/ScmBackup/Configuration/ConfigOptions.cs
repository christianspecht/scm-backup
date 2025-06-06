using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Configuration
{
    /// <summary>
    /// Main class for various options
    /// </summary>
    class ConfigOptions
    {
        public BackupOptions Backup { get; set; } = new BackupOptions();
    }

    class BackupOptions
    {
        public bool RemoveDeletedRepos { get; set; }
        public bool LogRepoFinished { get; set; }
        public bool AddTimestampedSubfolder { get; set; }
        public string TimestampFormat { get; set; }
        public string S3BucketName { get; set; }
    }
}
