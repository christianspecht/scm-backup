using ScmBackup.Hosters;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterBackup : IBackup
    {
        public bool WasExecuted { get; private set; }

        public bool Result { get; set; }

        public bool MakeBackup(HosterRepository repo, Config config, string repoFolder)
        {
            this.WasExecuted = true;
            return this.Result;
        }
    }
}
