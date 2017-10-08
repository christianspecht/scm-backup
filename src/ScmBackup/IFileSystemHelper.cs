namespace ScmBackup
{
    /// <summary>
    /// helper class for file system operations
    /// </summary>
    public interface IFileSystemHelper
    {
        bool DirectoryIsEmpty(string path);
    }
}