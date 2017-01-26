namespace ScmBackup.Scm
{
    internal interface IScm
    {
        /// <summary>
        /// Short name of the SCM (used to find settings in the config)
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// "Pretty" name for displaying
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Checks whether the SCM is present on this computer
        /// </summary>
        bool IsOnThisComputer(Config config);

        /// <summary>
        /// Checks whether the given directory is a repository
        /// </summary>
        bool DirectoryIsRepository(string directory);

        /// <summary>
        /// Creates a repository in the given directory
        /// </summary>
        void CreateRepository(string directory);
    }
}
