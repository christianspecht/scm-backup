using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Configuration
{
    /// <summary>
    /// Main class for various options
    /// </summary>
    class ConfigOptions
    {
        public ConfigOptions()
        {
            this.Git = new GitOptions();
        }

        public GitOptions Git { get; set; }
    }

    class GitOptions
    {
        /// <summary>
        /// Git implementation that will be used for all Git operations
        /// </summary>
        public string Implementation { get; set; }
    }
}
