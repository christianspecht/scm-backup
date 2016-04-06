namespace ScmBackup.Tests.Hosters
{
    internal class FakeHoster : BaseHoster
    {
        public FakeHoster()
        {
            this.Validator = new FakeConfigSourceValidator();
        }

        public override string Name
        {
            get { return "fakehoster"; }
        }
    }
}
