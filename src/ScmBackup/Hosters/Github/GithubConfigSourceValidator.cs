namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// validator for GitHub repositories
    /// </summary>
    internal class GithubConfigSourceValidator : IGithubConfigSourceValidator
    {
        public ValidationResult Validate(ConfigSource config)
        {
            var result = new ValidationResult();

            if (config.Hoster != "github")
            {
                result.AddMessage(ErrorLevel.Error, string.Format(Resource.GetString("GithubWrongHoster"), config.Hoster));
            }

            if (config.Type != "user" && config.Type != "org")
            {
                result.AddMessage(ErrorLevel.Error, string.Format(Resource.GetString("GithubWrongType"), config.Type));
            }

            if (string.IsNullOrWhiteSpace(config.Name))
            {
                result.AddMessage(ErrorLevel.Error, Resource.GetString("GithubNameEmpty"));
            }

            if (string.IsNullOrWhiteSpace(config.AuthName))
            {
                result.AddMessage(ErrorLevel.Warn, Resource.GetString("GithubAuthNameEmpty"));
            }

            if (string.IsNullOrWhiteSpace(config.Password))
            {
                result.AddMessage(ErrorLevel.Warn, Resource.GetString("GithubPasswordEmpty"));
            }

            return result;
        }
    }
}
