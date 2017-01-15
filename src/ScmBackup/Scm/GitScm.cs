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
    }
}