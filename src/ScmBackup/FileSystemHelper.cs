using System;
using System.Collections.Generic;
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

        /// <summary>
        /// wrapper for Directory.CreateDirectory
        /// </summary>
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Creates a subdirectory inside the given directory and returns the path
        /// </summary>
        public string CreateSubDirectory(string mainDir, string subDir)
        {
            if (!Directory.Exists(mainDir))
            {
                throw new DirectoryNotFoundException(string.Format(Resource.DirectoryDoesntExist, mainDir));
            }

            string newDir = Path.Combine(mainDir, subDir);
            Directory.CreateDirectory(newDir);
            return newDir;
        }

        /// <summary>
        /// wrapper for Path.Combine
        /// </summary>
        public string PathCombine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        /// <summary>
        /// Returns a list of all subdirectory names
        /// </summary>
        public IEnumerable<string> GetSubDirectoryNames(string path)
        {
            var info = new DirectoryInfo(path);
            return info.GetDirectories().Select(x => x.Name);
        }

        /// <summary>
        /// Deletes a directory
        /// </summary>
        public void DeleteDirectory(string path)
        {
            // if the directory is a Git repo which was pulled into, Directory.Delete isn't able to delete it: https://stackoverflow.com/q/63449326/6884
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };

            foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                info.Attributes = FileAttributes.Normal;
            }

            directory.Delete(true);
        }
    }
}
