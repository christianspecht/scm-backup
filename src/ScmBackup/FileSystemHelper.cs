using System.IO;
using System.Linq;

namespace ScmBackup
{
    /// <summary>
    /// helper class for file system operations
    /// </summary>
    public class FileSystemHelper : IFileSystemHelper
    {
        /// <summary>
        /// Checks whether the given directory is empty
        /// </summary>
        public bool DirectoryIsEmpty(string path)
        {
            if (Directory.GetFiles(path).Any() || Directory.GetDirectories(path).Any())
            {
                return false;
            }

            return true;
        }
    }
}
