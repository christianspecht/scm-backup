using System;
using System.IO;

namespace ScmBackup.Scm
{
    [Scm(Type = ScmType.Git)]
    internal class GitScm : CommandLineScm, IScm
    {
        public override string ShortName
        {
            get { return "git"; }
        }

        public override string DisplayName
        {
            get { return "Git"; }
        }

        protected override string CommandName
        {
            get { return "git"; }
        }

        protected override bool IsOnThisComputer()
        {
            string result = this.ExecuteCommand("--version");
            return result.ToLower().Contains("git version");
        }

        public override string GetVersionNumber()
        {
            string result = this.ExecuteCommand("--version");

            const string search = "git version ";
            return result.Substring(result.IndexOf(search) + search.Length).Replace("\n", "");
        }

        public override bool DirectoryIsRepository(string directory)
        {
            // SCM Backup uses bare repos only, so we don't need to check for non-bare repos at all
            string cmd = string.Format("-C \"{0}\" rev-parse --is-bare-repository", directory);
            string result = this.ExecuteCommand(cmd);
            return result.ToLower().StartsWith("true");
        }

        public override void CreateRepository(string directory)
        {
            if (!this.DirectoryIsRepository(directory))
            {
                string cmd = string.Format("init --bare \"{0}\"", directory);
                this.ExecuteCommand(cmd);
            }
        }

        public override void PullFromRemote(string remoteUrl, string directory)
        {
            if (!this.DirectoryIsRepository(directory))
            {
                if (Directory.Exists(directory) && !FileSystemHelper.DirectoryIsEmpty(directory))
                {
                    // TODO: change to Resource.ScmTargetDirectoryNotEmpty when Visual Studio starts updating Resource.Designer.cs again
                    throw new InvalidOperationException(string.Format("Target directory is not empty: {0}", directory));
                }
                
                this.CreateRepository(directory);
            }
            
            string cmd = string.Format("fetch --force --prune {0} refs/heads/*:refs/heads/* refs/tags/*:refs/tags/*", remoteUrl);
            this.ExecuteCommand(cmd);
        }

        public override bool RepositoryContainsCommit(string directory, string commitid)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException();
            }

            if (!this.DirectoryIsRepository(directory))
            {
                throw new InvalidOperationException();
            }

            // https://stackoverflow.com/a/21878920/6884
            string cmd = "rev-parse --quiet --verify " + commitid + "^{commit}";
            string result = this.ExecuteCommand(cmd);

            if (result.StartsWith(commitid))
            {
                return true;
            }

            return false;
        }
    }
}