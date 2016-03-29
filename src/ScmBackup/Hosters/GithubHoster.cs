namespace ScmBackup.Hosters
{
    /// <summary>
    /// BaseHoster implementation for GitHub
    /// </summary>
    internal class GithubHoster : BaseHoster
    {
        public override string Name
        {
            get { return "github"; }
        }
    }
}
