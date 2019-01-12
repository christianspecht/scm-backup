namespace ScmBackup.Configuration
{
    /// <summary>
    /// Configuration data for email sending
    /// </summary>
    public class ConfigEmail
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
