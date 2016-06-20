using ScmBackup.Hosters;

namespace ScmBackup.Tests.Hosters
{
    [Hoster(Name = "fake")]
    internal class FakeHoster : IHoster
    {
        public FakeHoster()
        {
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

        public IConfigSourceValidator Validator { get; private set; }
    }
}
