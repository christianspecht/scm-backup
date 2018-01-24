using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ScmBackup.Scm
{
    internal abstract class CommandLineScm : IScm
    {
        private string executable;

        public abstract string ShortName { get; }

        public abstract string DisplayName { get; }

        protected abstract IContext context { get; set; }

        /// <summary>
        /// The command that needs to be called
        /// </summary>
        protected abstract string CommandName { get; }

        /// <summary>
        /// Check whether the SCM exists on this computer
        /// Must be implemented in the child classes by calling ExecuteCommand and checking the result.
        /// </summary>
        public abstract bool IsOnThisComputer();

        /// <summary>
        /// Gets the SCM's version number.
        /// Must be implemented in the child classes by calling ExecuteCommand and checking the result.
        /// Should throw exceptions if the version number can't be determined.
        /// </summary>
        public abstract string GetVersionNumber();

        /// <summary>
        /// Executes the command line tool.
        /// GetExecutable must already have been called before (usually by calling IsOnThisComputer)
        /// </summary>
        protected CommandLineResult ExecuteCommand(string args)
        {
            if (string.IsNullOrWhiteSpace(this.executable))
            {
                throw new InvalidOperationException("run GetExecutable() first");
            }

            var info = new ProcessStartInfo();
            info.FileName = this.executable;
            info.Arguments = args;
            info.CreateNoWindow = true;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;

            var proc = Process.Start(info);
            var result = new CommandLineResult();
            result.StandardError = proc.StandardError.ReadToEnd();
            result.StandardOutput = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();

            result.ExitCode = proc.ExitCode;
            return result;
        }

        /// <summary>
        /// Checks whether the SCM is present on this computer
        /// </summary>
        public bool IsOnThisComputer(Config config)
        {
            this.GetExecutable(config);
            return this.IsOnThisComputer();
        }

        /// <summary>
        /// Checks whether the given directory is a repository
        /// Must be implemented in the child classes by calling ExecuteCommand and checking the result.
        /// </summary>
        public abstract bool DirectoryIsRepository(string directory);

        /// <summary>
        /// Creates a repository in the given directory
        /// Must be implemented in the child classes by calling ExecuteCommand and checking the result.
        /// </summary>
        public abstract void CreateRepository(string directory);

        /// <summary>
        /// Pulls from a remote repository into a local folder.
        /// If the folder doesn't exist or is not a repository, it's created first.
        /// Must be implemented in the child classes by calling ExecuteCommand and checking the result.
        /// </summary>
        public abstract void PullFromRemote(string remoteUrl, string directory);

        /// <summary>
        /// Checks whether the repo in this directory contains a commit with this ID
        /// Must be implemented in the child classes by calling ExecuteCommand and checking the result.
        /// </summary>
        public abstract bool RepositoryContainsCommit(string directory, string commitid);

        /// <summary>
        /// Gets the file to execute
        /// (either a complete path from the config, or this.CommandName)
        /// </summary>
        private void GetExecutable(Config config)
        {
            this.executable = this.CommandName;

            // check if there's an path in the "Scms" section in the config
            // (if it's there, the file MUST exist!)
            if (config.Scms != null)
            {
                var configValue = config.Scms.FirstOrDefault(s => s.Name.ToLower() == this.ShortName.ToLower());
                if (configValue != null)
                {
                    if (!File.Exists(configValue.Path))
                    {
                        throw new FileNotFoundException(string.Format(Resource.ScmNotOnThisComputer + ": {1}", this.DisplayName, configValue.Path));
                    }

                    this.executable = configValue.Path;
                }
            }
        }
    }
}
