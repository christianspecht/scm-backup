namespace ScmBackup.Hosters
{
    /// <summary>
    /// validator for GitHub repositories
    /// </summary>
    internal class GithubConfigSourceValidator : IConfigSourceValidator
    {
        public ValidationResult Validate(ConfigSource config)
        {
            var result = new ValidationResult();

            if (config.Hoster != "github")
            {
                result.AddMessage(ErrorLevel.Error, "wrong hoster");
            }

            if (config.Type != "user" && config.Type != "org")
            {
                result.AddMessage(ErrorLevel.Error, "wrong type");
            }

            if (config.Name == "")
            {
                result.AddMessage(ErrorLevel.Error, "name is empty");
            }

            return result;
        }
    }
}
