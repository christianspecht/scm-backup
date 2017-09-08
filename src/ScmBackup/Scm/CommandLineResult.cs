namespace ScmBackup.Scm
{
    /// <summary>
    /// return value for CommandLineScm
    /// </summary>
    internal class CommandLineResult
    {
        public CommandLineResult()
        {
            this.ExitCode = int.MinValue;
        }

        /// <summary>
        /// The error output of the command
        /// </summary>
        public string StandardError { get; set; }

        /// <summary>
        /// The standard output of the command
        /// </summary>
        public string StandardOutput { get; set; }

        /// <summary>
        /// The exit code of the command
        /// </summary>
        public int ExitCode { get; set; }

        /// <summary>
        /// The output (standard or error, whichever was set) of the command
        /// </summary>
        public string Output
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.StandardError) ? this.StandardOutput : this.StandardError;
            }
        }

        /// <summary>
        /// Did the command execute successfully?
        /// </summary>
        public bool Successful
        {
            get
            {
                return (this.ExitCode == 0);
            }
        }
    }
}
