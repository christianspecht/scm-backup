namespace ScmBackup.Tests
{
    internal class FakeHosterValidator : IHosterValidator
    {
        public int ValidationCounter { get; private set; }
        public bool WasValidated { get; private set; }
        public ValidationResult Result { get; set; }

        public FakeHosterValidator()
        {
            this.Result = new ValidationResult();
            this.ValidationCounter = 0;
        }

        public ValidationResult Validate(ConfigSource config)
        {
            this.WasValidated = true;
            this.ValidationCounter++;
            return this.Result;
        }
    }
}
