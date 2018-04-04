namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// validator for GitHub repositories
    /// </summary>
    internal class GithubConfigSourceValidator : ConfigSourceValidatorBase
    {
        public override string HosterName
        {
            get { return "github"; }
        }
    }
}
