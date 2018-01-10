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

        Config Config { get; }
    }
}
