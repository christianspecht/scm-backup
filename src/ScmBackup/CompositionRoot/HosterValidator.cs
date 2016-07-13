namespace ScmBackup.CompositionRoot
{
    internal class HosterValidator : IHosterValidator
    {
        private readonly IHosterFactory factory;

        public HosterValidator(IHosterFactory factory)
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
