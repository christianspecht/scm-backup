namespace ScmBackup.Hosters
{
    /// <summary>
    /// marker interface for ConfigSource validators
    /// </summary>
    internal interface IConfigSourceValidator
    {
        bool AuthNameAndNameMustBeEqual { get; }
        ValidationResult Validate(ConfigSource config);
    }
}

