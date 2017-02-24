namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// BaseHoster implementation for GitHub
    /// </summary>
    [Hoster(Name = "github")]
    internal class GithubHoster : IHoster
    {
        public GithubHoster(IConfigSourceValidator validator, IHosterApi api)
        {
            this.Validator = validator;
            this.Api = api;
        }

        public IConfigSourceValidator Validator { get; private set; }
        public IHosterApi Api { get; private set; }
    }
}
