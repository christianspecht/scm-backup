namespace ScmBackup.CompositionRoot
{
    internal class HosterValidator : IHosterValidator
    {
        private readonly HosterFactory factory;

        public HosterValidator(HosterFactory factory)
        {
            this.factory = factory;
        }

        public ValidationResult Validate(ConfigSource source)
        {
            var hoster = this.factory.Create(source.Hoster);
            return hoster.Validator.Validate(source);
        }
    }
}
