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
    }
}