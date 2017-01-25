using System;
using System.Globalization;
using System.IO;

namespace ScmBackup.Tests.Integration
{
    /// <summary>
    /// helper class to create unique temp directories for integration tests
    /// </summary>
    public class TempDirectoryHelper
    {
        public static string CreateTempDirectory()
        {
            string tempDir = Path.GetTempPath();
            string newDir = "scm-backup-temp-" + DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            string finalDir = Path.Combine(tempDir, newDir);

            if (Directory.CreateDirectory(finalDir) != null)
            {
                return finalDir;
            }

            return string.Empty;
        }
    }
}
