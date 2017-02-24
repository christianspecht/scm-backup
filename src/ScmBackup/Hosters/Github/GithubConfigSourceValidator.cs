namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// validator for GitHub repositories
    /// </summary>
    internal class GithubConfigSourceValidator : IConfigSourceValidator
    {
        public ValidationResult Validate(ConfigSource config)
        {
            var result = new ValidationResult(config);

            if (config.Hoster != "github")
            {
                result.AddMessage(ErrorLevel.Error, string.Format(Resource.WrongHoster, config.Hoster));
            }

            if (config.Type != "user" && config.Type != "org")
            {
                result.AddMessage(ErrorLevel.Error, string.Format(Resource.WrongType, config.Type));
            }

            if (string.IsNullOrWhiteSpace(config.Name))
            {
                result.AddMessage(ErrorLevel.Error, Resource.NameEmpty);
            }

            bool authNameEmpty = string.IsNullOrWhiteSpace(config.AuthName);
            bool passwordEmpty = string.IsNullOrWhiteSpace(config.Password);

            if (authNameEmpty != passwordEmpty)
            {
                result.AddMessage(ErrorLevel.Error, Resource.AuthNameOrPasswortEmpty);
            }
            else if (authNameEmpty && passwordEmpty)
            {
                result.AddMessage(ErrorLevel.Warn, Resource.AuthNameAndPasswortEmpty);
            }

            return result;
        }
    }
}
