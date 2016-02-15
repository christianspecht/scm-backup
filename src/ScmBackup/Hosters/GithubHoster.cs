namespace ScmBackup.Hosters
{
    /// <summary>
    /// IHoster implementation for GitHub
    /// </summary>
    public class GithubHoster : IHoster
    {
        public string Name
        {
            get { return "github"; }
        }
    }
}
