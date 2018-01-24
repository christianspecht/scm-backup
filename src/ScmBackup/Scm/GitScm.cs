using System;
using System.IO;

namespace ScmBackup.Scm
{
    [Scm(Type = ScmType.Git)]
    internal class GitScm : CommandLineScm, IScm
    {
        public GitScm(IFileSystemHelper filesystemhelper, IContext context)
        {
            this.FileSystemHelper = filesystemhelper;
            this.context = context;
        }

        public IFileSystemHelper FileSystemHelper { get; set; }

        protected override IContext context { get; set; }

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

        public override bool IsOnThisComputer()
        {
            var result = this.ExecuteCommand("--version");

            if (result.Successful && result.StandardOutput.ToLower().Contains("git version"))
            {
                return true;
            }

            return false;
        }

        public override string GetVersionNumber()
        {
            var result = this.ExecuteCommand("--version");

            if (result.Successful)
            {
                const string search = "git version ";
                return result.StandardOutput.Substring(result.StandardOutput.IndexOf(search) + search.Length).Replace("\n", "");
            }

            throw new InvalidOperationException(result.Output);
        }

        public override bool DirectoryIsRepository(string directory)
        {
            // SCM Backup uses bare repos only, so we don't need to check for non-bare repos at all
            string cmd = string.Format("-C \"{0}\" rev-parse --is-bare-repository", directory);
            var result = this.ExecuteCommand(cmd);

            if (result.Successful && result.StandardOutput.ToLower().StartsWith("true"))
            {
                return true;
            }

            return false;
        }

        public override void CreateRepository(string directory)
        {
            if (!this.DirectoryIsRepository(directory))
            {
                string cmd = string.Format("init --bare \"{0}\"", directory);
                var result = this.ExecuteCommand(cmd);

                if (!result.Successful)
                {
                    throw new InvalidOperationException(result.Output);
                }
            }
        }

        public override void PullFromRemote(string remoteUrl, string directory)
        {
            if (!this.DirectoryIsRepository(directory))
            {
                if (Directory.Exists(directory) && !this.FileSystemHelper.DirectoryIsEmpty(directory))
                {
                    throw new InvalidOperationException(string.Format(Resource.ScmTargetDirectoryNotEmpty, directory));
                }
                
                this.CreateRepository(directory);
            }
            
            string cmd = string.Format("-C \"{0}\" fetch --force --prune {1} refs/heads/*:refs/heads/* refs/tags/*:refs/tags/*", directory, remoteUrl);
            var result = this.ExecuteCommand(cmd);

            if (!result.Successful)
            {
                throw new InvalidOperationException(result.Output);
            }
        }

        public override bool RepositoryContainsCommit(string directory, string commitid)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(string.Format(Resource.DirectoryDoesntExist, directory));
            }

            if (!this.DirectoryIsRepository(directory))
            {
                throw new InvalidOperationException(string.Format(Resource.DirectoryNoRepo, directory));
            }

            // https://stackoverflow.com/a/21878920/6884
            string cmd = string.Format("-C \"{0}\" rev-parse --quiet --verify {1}^{{commit}}", directory, commitid);
            var result = this.ExecuteCommand(cmd);

            if (result.Successful && result.Output.StartsWith(commitid))
            {
                return true;
            }

            return false;
        }
    }
}