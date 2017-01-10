using ScmBackup.Scm;

namespace ScmBackup.Tests.Scm
{
    internal class FakeScm : IScm
    {
        /// <summary>
        /// Value returned by IsOnThisComputer()
        /// </summary>
        public bool IsOnThisComputerResult { get; set; }

        public string ShortName
        {
            get { return "fake"; }
        }

        public string DisplayName
        {
            get { return "Fake"; }
        }

        public bool IsOnThisComputer(Config config)
        {
            return this.IsOnThisComputerResult;
        }
    }
}
