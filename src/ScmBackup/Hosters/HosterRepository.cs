namespace ScmBackup.Hosters
{
    /// <summary>
    /// Data to access one single repository
    /// </summary>
    internal class HosterRepository
    {
        public HosterRepository(string name, string cloneUrl, ScmType scm)
        {
            this.Name = name.Replace('/', '#');
            this.CloneUrl = cloneUrl;
            this.Scm = scm;
        }

        /// <summary>
        /// Name of the repository
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// URL to clone the repository
        /// </summary>
        public string CloneUrl { get; private set; }

        /// <summary>
        /// The SCM of the repository
        /// </summary>
        public ScmType Scm { get; private set; }
    }
}
