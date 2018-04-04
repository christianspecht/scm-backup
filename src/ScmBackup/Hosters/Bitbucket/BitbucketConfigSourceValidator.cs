namespace ScmBackup.Hosters.Bitbucket
{
    /// <summary>
    /// validator for Bitbucket repositories
    /// </summary>
    internal class BitbucketConfigSourceValidator : ConfigSourceValidatorBase
    {
        public override string HosterName
        {
            get { return "bitbucket"; }
        }
    }
}
