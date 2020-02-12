using System.Collections.Generic;

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

        /// <summary>
        /// The "To" value can contain multiple emails separated with ;
        /// This will return a list of them
        /// </summary>
        public IEnumerable<string> To_AsList()
        {
            return this.To.Split(";");
        }
    }
}
