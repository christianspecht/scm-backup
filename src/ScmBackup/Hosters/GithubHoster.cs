namespace ScmBackup.Hosters
{
    /// <summary>
    /// BaseHoster implementation for GitHub
    /// </summary>
    [Hoster(Name = "github")]
    internal class GithubHoster : IHoster
    {
        public GithubHoster(IGithubConfigSourceValidator validator)
        {
            this.Validator = validator;
        }

        public IConfigSourceValidator Validator { get; private set; }
    }
}
