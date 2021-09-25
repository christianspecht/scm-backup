using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// helper class for file system operations
    /// </summary>
    public interface IFileSystemHelper
    {
        /// <summary>
        /// Checks whether the given directory is empty
        /// </summary>
        bool DirectoryIsEmpty(string path);

        /// <summary>
        /// Creates a subdirectory inside the given directory and returns the path
        /// </summary>
        string CreateSubDirectory(string mainDir, string subDir);

        /// <summary>
        /// wrapper for Path.Combine
        /// </summary>
        string PathCombine(string path1, string path2);

        /// <summary>
        /// Returns a list of all subdirectory names
        /// </summary>
        IEnumerable<string> GetSubDirectoryNames(string path);

        /// <summary>
        /// Deletes a directory
        /// </summary>
        void DeleteDirectory(string path);
    }
}