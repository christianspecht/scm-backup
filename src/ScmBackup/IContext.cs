using ScmBackup.Configuration;
using System;

namespace ScmBackup
{
    /// <summary>
    /// "application context" for global information
    /// </summary>
    internal interface IContext
    {
        Version VersionNumber { get; }

        string VersionNumberString { get; }

        string AppTitle { get; }

        /// <summary>
        /// "short version" of the app title, valid for HTTP user agent
        /// </summary>
        string UserAgent { get; }

        Config Config { get; }
    }
}
