namespace ScmBackup.Hosters.Github
{
    /// <summary>
    /// validator for GitHub repositories
    /// </summary>
    internal class GithubConfigSourceValidator : IConfigSourceValidator
    {
        public ValidationResult Validate(ConfigSource source)
        {
            var result = new ValidationResult(source);

            if (source.Hoster != "github")
            {
                result.AddMessage(ErrorLevel.Error, string.Format(Resource.WrongHoster, source.Hoster));
            }

            if (source.Type != "user" && source.Type != "org")
            {
                result.AddMessage(ErrorLevel.Error, string.Format(Resource.WrongType, source.Type));
            }

            if (string.IsNullOrWhiteSpace(source.Name))
            {
                result.AddMessage(ErrorLevel.Error, Resource.NameEmpty);
            }

            bool authNameEmpty = string.IsNullOrWhiteSpace(source.AuthName);
            bool passwordEmpty = string.IsNullOrWhiteSpace(source.Password);

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
