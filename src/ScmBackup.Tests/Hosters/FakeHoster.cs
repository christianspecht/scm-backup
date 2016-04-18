namespace ScmBackup.Tests.Hosters
{
    internal class FakeHoster : BaseHoster
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

        public override string Name
        {
            get { return "fake"; }
        }
    }
}
