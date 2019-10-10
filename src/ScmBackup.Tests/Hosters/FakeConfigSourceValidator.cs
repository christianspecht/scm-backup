using ScmBackup.Hosters;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeConfigSourceValidator : IConfigSourceValidator
    {
        public bool WasValidated { get; private set; }

        public bool AuthNameAndNameMustBeEqual { get; }

        public ValidationResult Result { get; set; }

        public ValidationResult Validate(ConfigSource config)
        {
            this.WasValidated = true;
            return this.Result;
        }
    }
}
