namespace ScmBackup
{
    /// <summary>
    /// Configuration data to get the repositories of user X from hoster Y
    /// (subclass for Config)
    /// </summary>
    internal class ConfigSource
    {
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
    }
}
