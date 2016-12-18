namespace ScmBackup
{
    /// <summary>
    /// Configuration data to get the repositories of user X from hoster Y
    /// (subclass for Config)
    /// </summary>
    internal class ConfigSource
    {
        /// <summary>
        /// title of this config source (must be unique)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// name of the hoster
        /// </summary>
        public string Hoster { get; set; }

        /// <summary>
        /// user type (e.g. user/team)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// user name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// user name for authentication
        /// (can be a different than the user whose repositories are backed up)
        /// </summary>
        public string AuthName { get; set; }

        /// <summary>
        /// password for authentication
        /// </summary>
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var source = obj as ConfigSource;

            if (source == null)
            {
                return false;
            }

            return (source.Title == this.Title);
        }

        public override int GetHashCode()
        {
            return this.Title.GetHashCode();
        }
    }
}
