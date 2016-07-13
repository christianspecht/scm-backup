namespace ScmBackup
{
    internal interface IHosterValidator
    {
        ValidationResult Validate(ConfigSource source);
    }
}
