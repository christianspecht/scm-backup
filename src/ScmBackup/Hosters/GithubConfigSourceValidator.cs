namespace ScmBackup.Hosters
{
    internal class GithubConfigSourceValidator : IConfigSourceValidator
    {
        public ValidationResult Validate(ConfigSource config)
        {
            var result = new ValidationResult();

            if (config.Hoster != "github")
            {
                result.AddMessage(ErrorLevel.Error, "wrong hoster");
            }

            return result;
        }
    }
}
