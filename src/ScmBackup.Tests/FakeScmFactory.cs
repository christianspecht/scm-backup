using ScmBackup.Scm;
using System;
using System.Collections.Generic;

namespace ScmBackup.Tests
{
    internal class FakeScmFactory : Dictionary<ScmType, IScm>, IScmFactory
    {
        public void Register(ScmType type, IScm scm)
        {
            this.Add(type, scm);
        }

        public IScm Create(ScmType type)
        {
            IScm result;
            if (!this.TryGetValue(type, out result))
            {
                throw new InvalidOperationException();
            }
            return result;
        }
    }
}
