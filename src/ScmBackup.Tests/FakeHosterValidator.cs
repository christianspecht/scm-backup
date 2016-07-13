namespace ScmBackup.Tests
{
    internal class FakeHosterValidator : IHosterValidator
    {
        public bool WasValidated { get; private set; }
        public ValidationResult Result { get; set; }

        public FakeHosterValidator()
        {
            this.Result = new ValidationResult();
        }

        public ValidationResult Validate(ConfigSource config)
        {
            this.WasValidated = true;
            return this.Result;
        }
    }
}
