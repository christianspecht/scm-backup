using System;
using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// factory which creates IHoster instances
    /// </summary>
    internal class HosterFactory : Dictionary<string, IHoster>, IHosterFactory
    {
        public void Add(IHoster hoster)
        {
            this.Add(hoster.Name, hoster);
        }

        public IHoster Create(string hosterName)
        {
            IHoster result;

            if (!this.TryGetValue(hosterName, out result))
            {
                throw new InvalidOperationException(string.Format("Hoster {0} doesn't exist", hosterName));
            }

            return result;
        }
    }
}
