using System;

namespace ScmBackup.Tests
{
    public class FakeScmBackup : IScmBackup
    {
        public Exception ToThrow { get; set; }

        public void Run()
        {
            if (this.ToThrow != null)
            {
                throw this.ToThrow;
            }
        }
    }
}
