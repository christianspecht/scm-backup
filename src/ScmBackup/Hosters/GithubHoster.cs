namespace ScmBackup.Hosters
{
    /// <summary>
    /// BaseHoster implementation for GitHub
    /// </summary>
    [Hoster(Name = "github")]
    internal class GithubHoster : IHoster
    {
        public GithubHoster()
        {
            this.Validator = new GithubConfigSourceValidator();
        }

        public IConfigSourceValidator Validator { get; private set; }
    }
}
