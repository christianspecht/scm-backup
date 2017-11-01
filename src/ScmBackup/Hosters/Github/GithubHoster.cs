namespace ScmBackup.Hosters.Github
{
    internal class GithubHoster : IHoster
    {
        public GithubHoster(IConfigSourceValidator validator, IHosterApi api, IBackup backup)
        {
            this.Validator = validator;
            this.Api = api;
            this.Backup = backup;
        }

        public IConfigSourceValidator Validator { get; private set; }
        public IHosterApi Api { get; private set; }
        public IBackup Backup { get; private set; }
    }
}
