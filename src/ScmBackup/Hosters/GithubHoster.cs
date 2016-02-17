namespace ScmBackup.Hosters
{
    /// <summary>
    /// IHoster implementation for GitHub
    /// </summary>
    internal class GithubHoster : IHoster
    {
        public string Name
        {
            get { return "github"; }
        }
    }
}
