using System;
using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// Interface for logging
    /// </summary>
    internal interface ILogger
    {
        void Log(ErrorLevel level, string message, params object[] arg);
        void Log(ErrorLevel level, Exception ex, string message, params object[] arg);

        /// <summary>
        /// List of files to backup (for example, the logger's config file)
        /// </summary>
        List<string> FilesToBackup { get; }

        /// <summary>
        /// This will be executed when SCM Backup exits
        /// </summary>
        void ExecuteOnExit();
    }
}
