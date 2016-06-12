using ScmBackup.Hosters;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHoster : IHoster
    {
        public FakeHoster()
        {
            this.Name = "fake";
            this.Validator = new FakeConfigSourceValidator();
            this.FakeValidator.Result = new ValidationResult();
        }

        /// <summary>
        /// easier access (without casting) to the fake validator
        /// </summary>
        public FakeConfigSourceValidator FakeValidator
        {
            get { return (FakeConfigSourceValidator)this.Validator; }
        }

        public string Name { get; private set; }
        public IConfigSourceValidator Validator { get; private set; }
    }
}
