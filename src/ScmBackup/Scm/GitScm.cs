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
    }
}