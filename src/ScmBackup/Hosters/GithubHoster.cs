namespace ScmBackup.Hosters
{
    /// <summary>
    /// BaseHoster implementation for GitHub
    /// </summary>
    internal class GithubHoster : BaseHoster
    {
        public GithubHoster()
        {
            this.Validator = new GithubConfigSourceValidator();
        }

        public override string Name
        {
            get { return "github"; }
        }
    }
}
