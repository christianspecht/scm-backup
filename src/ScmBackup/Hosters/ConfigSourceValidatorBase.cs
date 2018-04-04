namespace ScmBackup.Hosters
{
    /// <summary>
    /// base class for all config source validators
    /// </summary>
    internal abstract class ConfigSourceValidatorBase : IConfigSourceValidator
    {
        /// <summary>
        /// name of the hoster (the "hoster" value from the config source)
        /// </summary>
        public abstract string HosterName { get; }

        /// <summary>
        /// basic validation rules which are always the same
        /// </summary>
        public ValidationResult Validate(ConfigSource source)
        {
            var result = new ValidationResult(source);

            if (source.Hoster != this.HosterName)
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

            return this.ValidateSpecific(result, source);
        }

        /// <summary>
        /// hoster-specific validation rules - this CAN be implemented in the child classes IF the given hoster has special rules
        /// </summary>
        public ValidationResult ValidateSpecific(ValidationResult result, ConfigSource source)
        {
            return result;
        }
    }
}
