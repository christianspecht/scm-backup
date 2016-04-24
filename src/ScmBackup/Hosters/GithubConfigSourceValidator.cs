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
                result.AddMessage(ErrorLevel.Error, string.Format("wrong hoster: {0}", config.Hoster));
            }

            if (config.Type != "user" && config.Type != "org")
            {
                result.AddMessage(ErrorLevel.Error, string.Format("wrong type: {0}", config.Type));
            }

            if (config.Name == "")
            {
                result.AddMessage(ErrorLevel.Error, "name is empty");
            }

            return result;
        }
    }
}
