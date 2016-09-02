namespace ScmBackup.Hosters
{
    /// <summary>
    /// Data to access one single repository
    /// </summary>
    internal class HosterRepository
    {
        /// <summary>
        /// Name of the repository
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to clone the repository
        /// </summary>
        public string CloneUrl { get; set; }

        /// <summary>
        /// The SCM of the repository
        /// </summary>
        public ScmType Scm { get; set; }
    }
}
