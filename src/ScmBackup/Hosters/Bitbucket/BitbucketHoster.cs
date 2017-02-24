namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketHoster : IHoster
    {
        public BitbucketHoster(IConfigSourceValidator validator, IHosterApi api)
        {
            this.Validator = validator;
            this.Api = api;
        }

        public IConfigSourceValidator Validator { get; private set; }
        public IHosterApi Api { get; private set; }
    }
}
