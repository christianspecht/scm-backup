using ScmBackup.CompositionRoot;
using ScmBackup.Hosters;
using System;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHosterFactory : IHosterFactory
    {
        public IHoster FakeHoster { get; set; }
        public string LastHosterName { get; private set; }
        public bool CreateWasCalled { get; private set; }

        public FakeHosterFactory() { }

        public FakeHosterFactory(IHoster hoster)
        {
            this.FakeHoster = hoster;
        }

        public IHoster Create(string hosterName)
        {
            this.CreateWasCalled = true;
            this.LastHosterName = hosterName;

            if (this.FakeHoster == null)
            {
                throw new InvalidOperationException();
            }

            return this.FakeHoster;
        }
    }
}
