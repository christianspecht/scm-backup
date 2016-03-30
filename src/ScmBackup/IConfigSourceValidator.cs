namespace ScmBackup
{
    internal interface IConfigSourceValidator
    {
        ValidationResult Validate(ConfigSource config);
    }
}
